-- clinique_care_schema.sql
-- Connect to the 'clinique_care' database (in psql: \c clinique_care) or open a Query Tool in pgAdmin connected to the clinique_care DB.

-- CREATE TABLE statements
CREATE TABLE IF NOT EXISTS public.employees (
    id bigserial PRIMARY KEY,
    email varchar(320) NOT NULL,
    adresse varchar(255) NOT NULL,
    telephone varchar(50) NOT NULL,
    nom varchar(200) NOT NULL,
    prenom varchar(200) NOT NULL,
    cin varchar(50) NOT NULL,
    datenaissance date NULL,
    poste varchar(200) NOT NULL,
    salaire varchar(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.contracts (
    id bigserial PRIMARY KEY,
    type varchar(50) NOT NULL,
    employe varchar(255) NOT NULL,
    datedebut date NULL,
    datefin date NULL
);

CREATE TABLE IF NOT EXISTS public.presences (
    id bigserial PRIMARY KEY,
    employe varchar(255) NOT NULL,
    date date NULL,
    status varchar(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.leaves (
    id bigserial PRIMARY KEY,
    employe varchar(255) NOT NULL,
    type varchar(100) NOT NULL,
    debut date NULL,
    fin date NULL
);

CREATE TABLE IF NOT EXISTS public.payrolls (
    id bigserial PRIMARY KEY,
    employe varchar(255) NOT NULL,
    mois varchar(100) NOT NULL,
    montant varchar(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.trainings (
    id bigserial PRIMARY KEY,
    titre varchar(255) NOT NULL,
    employe varchar(255) NOT NULL,
    date date NULL
);

-- Seed data
INSERT INTO public.employees (email, adresse, telephone, nom, prenom, cin, datenaissance, poste, salaire)
VALUES
('sami@clinique.tn','Ariana','20202020','Sami','Ben Ali','12345678','1985-06-15','Médecin Généraliste','3500 TND')
ON CONFLICT DO NOTHING;

INSERT INTO public.employees (email, adresse, telephone, nom, prenom, cin, datenaissance, poste, salaire)
VALUES
('amina@clinique.tn','Tunis','29123456','Amina','Trabelsi','87654321','1990-03-02','Infirmière','1200 TND')
ON CONFLICT DO NOTHING;

INSERT INTO public.contracts (type, employe, datedebut, datefin)
VALUES
('CDI', 'Sami Ben Ali', '2025-01-01', '2026-01-01')
ON CONFLICT DO NOTHING;

INSERT INTO public.contracts (type, employe, datedebut, datefin)
VALUES
('CDD', 'Amina Trabelsi', '2025-06-01', '2025-12-01')
ON CONFLICT DO NOTHING;

INSERT INTO public.presences (employe, date, status)
VALUES
('Sami Ben Ali', '2025-10-01', 'Présent')
ON CONFLICT DO NOTHING;

INSERT INTO public.presences (employe, date, status)
VALUES
('Amina Trabelsi', '2025-10-01', 'Absent')
ON CONFLICT DO NOTHING;

INSERT INTO public.leaves (employe, type, debut, fin)
VALUES
('Sami Ben Ali', 'Annuel', '2025-08-01', '2025-08-10')
ON CONFLICT DO NOTHING;

INSERT INTO public.leaves (employe, type, debut, fin)
VALUES
('Amina Trabelsi', 'Maladie', '2025-09-05', '2025-09-08')
ON CONFLICT DO NOTHING;

INSERT INTO public.payrolls (employe, mois, montant)
VALUES
('Sami Ben Ali', 'Septembre 2025', '3500 TND')
ON CONFLICT DO NOTHING;

INSERT INTO public.payrolls (employe, mois, montant)
VALUES
('Amina Trabelsi', 'Septembre 2025', '1200 TND')
ON CONFLICT DO NOTHING;

INSERT INTO public.trainings (titre, employe, date)
VALUES
('Hygiène Hospitalière', 'Sami Ben Ali', '2025-11-15')
ON CONFLICT DO NOTHING;

