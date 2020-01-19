SET IDENTITY_INSERT RefOrderStatus ON
INSERT INTO RefOrderStatus ([Id],[Name])
VALUES (1,'Placed')

INSERT INTO RefOrderStatus ([Id],[Name])
VALUES (2,'Paid')

INSERT INTO RefOrderStatus ([Id],[Name])
VALUES (3,'Processing')

INSERT INTO RefOrderStatus ([Id],[Name])
VALUES (4,'Shipping')

INSERT INTO RefOrderStatus ([Id],[Name])
VALUES (5,'Complete')
SET IDENTITY_INSERT RefOrderStatus OFF

SET IDENTITY_INSERT Customer ON
INSERT INTO Customer ([Id], [Name], [Email])
VALUES (1, 'Raheem Sterling', 'rs@testemail.com')

INSERT INTO Customer ([Id], [Name], [Email])
VALUES (2, 'Leo Messi', 'lm@testemail.com')

INSERT INTO Customer ([Id],[Name], [Email])
VALUES (3, 'Cristiano Ronaldo', 'cr@testemail.com')
SET IDENTITY_INSERT Customer OFF

INSERT INTO [Order] ([CustomerId], [Price], [CreatedDate], [OrderStatusId])
VALUES (1, 10.00, GETDATE(), 1)

INSERT INTO [Order] ([CustomerId], [Price], [CreatedDate], [OrderStatusId])
VALUES (2, 20.00, (GETDATE()-1), 2)

INSERT INTO [Order] ([CustomerId], [Price], [CreatedDate], [OrderStatusId])
VALUES (3, 30.00, (GETDATE()-2), 3)

INSERT INTO [Order] ([CustomerId], [Price], [CreatedDate], [OrderStatusId])
VALUES (2, 40.00, (GETDATE()-3), 4)

INSERT INTO [Order] ([CustomerId], [Price], [CreatedDate], [OrderStatusId])
VALUES (1, 50.00, (GETDATE()-4), 5)