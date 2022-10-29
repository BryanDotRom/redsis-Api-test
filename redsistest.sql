-- ================================================================
--	lista de scripts para el funcionamiento de la api.
-- ================================================================

create database accountsTest

use accountsTest

create table countries(
	id int primary key,
	name varchar(10)
)

create table currencies(
	id int primary key,
	name varchar(5)
)

create table companies(
	id int primary key,
	name varchar(20),
	idCountry int foreign key references countries(id),
	idCurrencyLocal int foreign key references currencies(id)
)

create table accounts(
	id int identity(1,1) primary key,
	idCompany int foreign key references companies(id),
	company varchar(20) not null,
	idCountry int foreign key references countries(id),
	bank varchar(100) not null,
	account varchar(50) not null unique,
	idCurrencyLocal int foreign key references currencies(id),
	idCurrencyAccount int foreign key references currencies(id),
	userCreation varchar(100) not null,
)




-- INSERTS DE PRUEBA
-- ================================================================

INSERT INTO countries (id, name) VALUES
(1,	'Colombia'),
(2,	'Mexico'),
(3,	'Brasil'),
(4,	'Espa�a'),
(5,	'USA')

GO

INSERT INTO currencies (id, name) VALUES
(1,	'COP'),
(2,	'USD'),
(3,	'MNX'),
(4,	'EURO')

GO

INSERT INTO companies(id, name, idCountry, idCurrencyLocal) VALUES
(1,	'Coca-Cola',	1,	1),
(2,	'Redsis',	2,	3),
(3,	'Adidas',	5,	2)

GO

INSERT INTO accounts(idCompany, company, idCountry,	bank, account, idCurrencyLocal,	idCurrencyAccount, userCreation) VALUES
(1, 'Coca-Cola', 1, 'Bancolombia', '1515',	1, 1, 'Bryan'),
(2, 'Redsis', 2,	'Davivienda',	'8080', 3, 2, 'Jose'),
(1, 'Coca-Cola',	5, 'USA-bank', '246810', 2, 2, 'Carlos'),
(3, 'Adidas', 4, 'banco de barcelona',	'11223344',	4, 4,	'Maria')

GO

-- ======================================================
-- Description:	Get all accounts
-- ======================================================
CREATE PROCEDURE [dbo].[GetAccounts]	
AS
BEGIN	
   
	select a.id, a.idCompany, a.company, a.idCountry, cntr.name as country, a.bank, a.account, a.idCurrencyLocal, localCurr.name as currencyLocal, a.idCurrencyAccount, accCurr.name as currencyAccount, 0 as idUserCreation, a.userCreation
	from Accounts a inner join currencies localCurr
	on a.idCurrencyLocal = localCurr.id
	inner join currencies accCurr
	on a.idCurrencyAccount = accCurr.id
	inner join countries cntr
	on a.idCountry = cntr.id
END
GO

-- ======================================================
-- Description:	Get account info by Id
-- ======================================================
CREATE PROCEDURE [dbo].[GetAccountById] 
	@accountNumber int
AS
BEGIN	
   
	select a.id, a.idCompany, a.company, a.idCountry, cntr.name as country, a.bank, a.account, a.idCurrencyLocal, localCurr.name as currencyLocal, a.idCurrencyAccount, accCurr.name as currencyAccount, 0 as idUserCreation, a.userCreation
	from Accounts a inner join currencies localCurr
	on a.idCurrencyLocal = localCurr.id
	inner join currencies accCurr
	on a.idCurrencyAccount = accCurr.id
	inner join countries cntr
	on a.idCountry = cntr.id
	where a.account = @accountNumber
END
GO

-- ======================================================
-- Description:	Insert a new Account
-- ======================================================
CREATE PROCEDURE [dbo].[InsertAccount]
	
	@idCompany int ,
	@company varchar(20),
	@idCountry int,
	@bank varchar(100),
	@account varchar(50),
	@idCurrencyLocal int,
	@idCurrencyAccount int,
	@userCreation varchar(100)
AS
BEGIN	
   
	insert into accounts (idCompany, company, idCountry, bank, account, idCurrencyLocal, idCurrencyAccount, userCreation)
	values (@idCompany, @company, @idCountry, @bank, @account, @idCurrencyLocal, @idCurrencyAccount, @userCreation)


	select a.id, a.idCompany, a.company, a.idCountry, cntr.name as country, a.bank, a.account, a.idCurrencyLocal, localCurr.name as currencyLocal, a.idCurrencyAccount, accCurr.name as currencyAccount, 0 as idUserCreation, a.userCreation
	from Accounts a inner join currencies localCurr
	on a.idCurrencyLocal = localCurr.id
	inner join currencies accCurr
	on a.idCurrencyAccount = accCurr.id
	inner join countries cntr
	on a.idCountry = cntr.id
	where a.account = @account
END
GO

-- ======================================================
-- Description:	Update an existing account info
-- ======================================================
CREATE PROCEDURE [dbo].[UpdateAccount]
	@accountId int,
	@idCompany int,
	@company varchar(20),
	@idCountry int,
	@bank varchar(100),
	@account varchar(50),
	@idCurrencyLocal int,
	@idCurrencyAccount int,
	@userCreation varchar(100)
AS
BEGIN	
   
	update accounts
	SET idCompany = @idCompany, company = @company, idCountry = @idCountry, bank = @bank, account = @account, idCurrencyLocal = @idCurrencyLocal, idCurrencyAccount = @idCurrencyAccount, userCreation = @userCreation
	where id = @accountId
END
GO

-- ======================================================
-- Description:	Delete an existing account
-- ======================================================
CREATE PROCEDURE [dbo].[DeleteAccount]	
	@accountId int	
AS
BEGIN	
   DELETE FROM accounts WHERE id = @accountId;
END
GO

-- ======================================================
-- Description:	Get all Companies
-- ======================================================
CREATE PROCEDURE [dbo].[GetCompanies]	
AS
BEGIN	
   select com.id, com.name, com.idCountry, com.idCurrencyLocal, cntr.name as country, curr.name as currencyLocal 
	from companies com
	inner join countries cntr
	on com.idCountry = cntr.id
	inner join currencies curr
	on com.idCurrencyLocal = curr.id
END
GO