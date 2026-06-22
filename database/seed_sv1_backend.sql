USE DrugContraindicationDB;
GO

IF NOT EXISTS (SELECT 1 FROM Drugs WHERE DrugName = N'Paracetamol')
INSERT INTO Drugs (DrugName, Description, Manufacturer, ActiveIngredient)
VALUES (N'Paracetamol', N'Thuốc giảm đau, hạ sốt', N'DHG Pharma', N'Paracetamol');

IF NOT EXISTS (SELECT 1 FROM Drugs WHERE DrugName = N'Aspirin')
INSERT INTO Drugs (DrugName, Description, Manufacturer, ActiveIngredient)
VALUES (N'Aspirin', N'Thuốc giảm đau, chống viêm, chống kết tập tiểu cầu', N'Bayer', N'Acetylsalicylic Acid');

IF NOT EXISTS (SELECT 1 FROM Drugs WHERE DrugName = N'Ibuprofen')
INSERT INTO Drugs (DrugName, Description, Manufacturer, ActiveIngredient)
VALUES (N'Ibuprofen', N'Thuốc giảm đau, chống viêm không steroid', N'Generic', N'Ibuprofen');

IF NOT EXISTS (SELECT 1 FROM Drugs WHERE DrugName = N'Warfarin')
INSERT INTO Drugs (DrugName, Description, Manufacturer, ActiveIngredient)
VALUES (N'Warfarin', N'Thuốc chống đông máu', N'Generic', N'Warfarin');

IF NOT EXISTS (SELECT 1 FROM Drugs WHERE DrugName = N'Metformin')
INSERT INTO Drugs (DrugName, Description, Manufacturer, ActiveIngredient)
VALUES (N'Metformin', N'Thuốc điều trị đái tháo đường type 2', N'Generic', N'Metformin');

IF NOT EXISTS (SELECT 1 FROM Diseases WHERE DiseaseName = N'Viêm loét dạ dày')
INSERT INTO Diseases (DiseaseName, Description)
VALUES (N'Viêm loét dạ dày', N'Tình trạng tổn thương niêm mạc dạ dày, dễ chảy máu.');

IF NOT EXISTS (SELECT 1 FROM Diseases WHERE DiseaseName = N'Hen suyễn')
INSERT INTO Diseases (DiseaseName, Description)
VALUES (N'Hen suyễn', N'Bệnh hô hấp mạn tính gây khó thở, khò khè.');

IF NOT EXISTS (SELECT 1 FROM Diseases WHERE DiseaseName = N'Suy gan')
INSERT INTO Diseases (DiseaseName, Description)
VALUES (N'Suy gan', N'Tình trạng chức năng gan suy giảm.');

IF NOT EXISTS (SELECT 1 FROM Diseases WHERE DiseaseName = N'Suy thận')
INSERT INTO Diseases (DiseaseName, Description)
VALUES (N'Suy thận', N'Tình trạng chức năng thận suy giảm.');

IF NOT EXISTS (SELECT 1 FROM Diseases WHERE DiseaseName = N'Tăng huyết áp')
INSERT INTO Diseases (DiseaseName, Description)
VALUES (N'Tăng huyết áp', N'Bệnh lý tim mạch với huyết áp tăng cao kéo dài.');

IF NOT EXISTS (
    SELECT 1 FROM Contraindications
    WHERE DrugName = N'Aspirin' AND DiseaseName = N'Viêm loét dạ dày'
)
INSERT INTO Contraindications (DrugName, DiseaseName, Level, Warning)
VALUES (
    N'Aspirin',
    N'Viêm loét dạ dày',
    N'High',
    N'Aspirin có thể làm tăng nguy cơ xuất huyết tiêu hóa ở bệnh nhân viêm loét dạ dày.'
);

IF NOT EXISTS (
    SELECT 1 FROM Contraindications
    WHERE DrugName = N'Aspirin' AND DiseaseName = N'Hen suyễn'
)
INSERT INTO Contraindications (DrugName, DiseaseName, Level, Warning)
VALUES (
    N'Aspirin',
    N'Hen suyễn',
    N'Medium',
    N'Aspirin có thể làm nặng triệu chứng hen ở một số bệnh nhân nhạy cảm.'
);

IF NOT EXISTS (
    SELECT 1 FROM Contraindications
    WHERE DrugName = N'Paracetamol' AND DiseaseName = N'Suy gan'
)
INSERT INTO Contraindications (DrugName, DiseaseName, Level, Warning)
VALUES (
    N'Paracetamol',
    N'Suy gan',
    N'High',
    N'Paracetamol cần thận trọng hoặc tránh dùng ở bệnh nhân suy gan nặng.'
);

IF NOT EXISTS (
    SELECT 1 FROM Contraindications
    WHERE DrugName = N'Ibuprofen' AND DiseaseName = N'Suy thận'
)
INSERT INTO Contraindications (DrugName, DiseaseName, Level, Warning)
VALUES (
    N'Ibuprofen',
    N'Suy thận',
    N'High',
    N'Ibuprofen có thể làm giảm chức năng thận và không phù hợp với bệnh nhân suy thận.'
);

IF NOT EXISTS (
    SELECT 1 FROM Contraindications
    WHERE DrugName = N'Metformin' AND DiseaseName = N'Suy thận'
)
INSERT INTO Contraindications (DrugName, DiseaseName, Level, Warning)
VALUES (
    N'Metformin',
    N'Suy thận',
    N'High',
    N'Metformin có nguy cơ gây nhiễm toan lactic ở bệnh nhân suy thận.'
);

IF NOT EXISTS (
    SELECT 1 FROM Interactions
    WHERE DrugA = N'Warfarin' AND DrugB = N'Aspirin'
)
INSERT INTO Interactions (DrugA, DrugB, Level, Description)
VALUES (
    N'Warfarin',
    N'Aspirin',
    N'High',
    N'Kết hợp Warfarin và Aspirin có thể làm tăng nguy cơ chảy máu.'
);

IF NOT EXISTS (
    SELECT 1 FROM Interactions
    WHERE DrugA = N'Ibuprofen' AND DrugB = N'Aspirin'
)
INSERT INTO Interactions (DrugA, DrugB, Level, Description)
VALUES (
    N'Ibuprofen',
    N'Aspirin',
    N'Medium',
    N'Ibuprofen và Aspirin đều thuộc nhóm NSAID, có thể làm tăng nguy cơ kích ứng dạ dày.'
);

IF NOT EXISTS (
    SELECT 1 FROM Interactions
    WHERE DrugA = N'Paracetamol' AND DrugB = N'Warfarin'
)
INSERT INTO Interactions (DrugA, DrugB, Level, Description)
VALUES (
    N'Paracetamol',
    N'Warfarin',
    N'Medium',
    N'Paracetamol dùng kéo dài có thể ảnh hưởng đến tác dụng chống đông của Warfarin.'
);
GO