-- OP READ - LER --
SELECT * FROM Contas;
-- Procurando por contas cujo saldo seja maior que mil
SELECT Id,Titular,NumeroDaConta,Saldo FROM Contas
WHERE Saldo > 1000
GO
-- Procurando por contas que tem "Maria" no nome
SELECT * FROM Contas
WHERE Titular LIKE '%Maria%'
GO

SELECT * FROM Contas
WHERE NumeroDaConta = 1002
GO
-- Ordenar por saldo do maior p/ menor
SELECT * FROM Contas
ORDER BY Saldo DESC
GO 
-- Ordenar por saldo do menor p/ maior
SELECT * FROM Contas
ORDER BY Saldo ASC
GO 
-- Contar quantas contas existem na tabela
SELECT COUNT(*) AS TotalContas FROM Contas
GO
-- Soma dos saldos das contas na tabela
SELECT SUM (Saldo) AS SaldoTotal FROM Contas
GO
-- Média do saldo das contas
SELECT AVG(Saldo) AS MediaSaldos FROM Contas
GO