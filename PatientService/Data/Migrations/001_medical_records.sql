-- Apply to PatientDB before using the consultation and lab result endpoints.
CREATE TABLE IF NOT EXISTS medical_consultations (
    id uuid PRIMARY KEY,
    appointment_id uuid NOT NULL UNIQUE,
    patient_id uuid NOT NULL REFERENCES patients(id) ON DELETE CASCADE,
    doctor_id uuid NOT NULL,
    diagnosis varchar(2000),
    notes varchar(4000),
    prescribed_medicines varchar(4000),
    created_at timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS ix_medical_consultations_patient_created
    ON medical_consultations (patient_id, created_at DESC);

CREATE TABLE IF NOT EXISTS recommended_tests (
    id uuid PRIMARY KEY,
    consultation_id uuid NOT NULL REFERENCES medical_consultations(id) ON DELETE CASCADE,
    name varchar(200) NOT NULL,
    instructions varchar(2000),
    status varchar(100) NOT NULL DEFAULT 'Recommended',
    result varchar(4000),
    report_url varchar(1000),
    result_uploaded_at timestamp without time zone
);

CREATE INDEX IF NOT EXISTS ix_recommended_tests_consultation
    ON recommended_tests (consultation_id);
