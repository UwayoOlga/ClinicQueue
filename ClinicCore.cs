using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClinicQueue
{
    public enum PatientType { Adult, Child, Emergency }

    public abstract class Patient
    {
        public string Id { get; }
        public string Name { get; }
        public PatientType Type { get; }

        public Patient(string id, string name, PatientType type)
        {
            Id = id?.Trim() ?? throw new ArgumentNullException(nameof(id));
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("name can not be NULL or empty", nameof(name));
        }

        public abstract int GetPriority();

        public override string ToString()
            => $"{Type,-9} | {Name} | Id:{Id} | Priority:{GetPriority()}";
    }

    public class AdultPatient : Patient
    {
        public AdultPatient(string id, string name)
            : base(id, name, PatientType.Adult) { }
        public override int GetPriority() => 1;
    }

    public class ChildPatient : Patient
    {
        public ChildPatient(string id, string name)
            : base(id, name, PatientType.Child) { }
        public override int GetPriority() => 2;
    }

    public class EmergencyPatient : Patient
    {
        public EmergencyPatient(string id, string name)
            : base(id, name, PatientType.Emergency) { }
        public override int GetPriority() => 3;
    }

    public class VisitRecord
    {
        public DateTime Time { get; } = DateTime.Now;
        public string PatientId { get; }
        public string PatientName { get; }
        public PatientType Type { get; }

        public VisitRecord(Patient patient)
        {
            PatientId = patient.Id;
            PatientName = patient.Name;
            Type = patient.Type;
        }
        public override string ToString()
            => $"{Time:yyyy-MM-dd HH:mm} | Served: {PatientId} - {PatientName} ({Type})";
    }

    public class ClinicManager
    {
        private readonly Dictionary<string, Patient> _patients = new();
        private readonly List<Patient> _queue = new();
        private readonly List<VisitRecord> _recentVisits = new();

        public void RegisterPatient(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            if (_patients.ContainsKey(patient.Id))
                throw new InvalidOperationException("Patient already registered");
            _patients[patient.Id] = patient;
        }

        public void Enqueue(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("id required", nameof(patientId));
            patientId = patientId.Trim();
            if (!_patients.TryGetValue(patientId, out var p))
                throw new InvalidOperationException("Patient does not exist");
            if (_queue.Any(x => x.Id == patientId))
                throw new InvalidOperationException("Patient already on the queue");
            _queue.Add(p);
        }

        public Patient ServeNext()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("Queue is empty");
            var next = _queue.OrderByDescending(x => x.GetPriority()).First();
            _queue.Remove(next);
            _recentVisits.Add(new VisitRecord(next));
            return next;
        }

        public IReadOnlyList<Patient> GetQueue()
            => _queue.OrderByDescending(x => x.GetPriority()).ToList();

        public IReadOnlyList<VisitRecord> GetRecentVisits(int count = 5)
            => _recentVisits.TakeLast(count).ToList();

        public IReadOnlyList<Patient> ListPatients()
            => _patients.Values.OrderBy(x => x.Id).ToList();
    }
}