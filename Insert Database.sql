INSERT INTO Rols VALUES('Normal','Usuario normal')
INSERT INTO Rols VALUES('Mantenimiento','Usuario que realiza mantenimiento')
INSERT INTO Rols VALUES('Admin','Super usuario')

INSERT INTO Permissions VALUES('Create','Permiso de creacion')
INSERT INTO Permissions VALUES('Destroy','Permiso de eliminar')
INSERT INTO Permissions VALUES('Update','Permiso de actualizar')
INSERT INTO Permissions VALUES('Query','Permiso de consulta')

INSERT INTO Views VALUES('WorkOrders','Vista de gestion de ordenes de trabajo')

INSERT INTO ViewsPermissions VALUES(1,1)
INSERT INTO ViewsPermissions VALUES(2,1)
INSERT INTO ViewsPermissions VALUES(3,1)
INSERT INTO ViewsPermissions VALUES(4,1)

INSERT INTO RolsViewsPermissions VALUES(1,1)
INSERT INTO RolsViewsPermissions VALUES(1,4)
INSERT INTO RolsViewsPermissions VALUES(2,1)
INSERT INTO RolsViewsPermissions VALUES(2,3)
INSERT INTO RolsViewsPermissions VALUES(2,4)
INSERT INTO RolsViewsPermissions VALUES(3,1)
INSERT INTO RolsViewsPermissions VALUES(3,2)
INSERT INTO RolsViewsPermissions VALUES(3,3)
INSERT INTO RolsViewsPermissions VALUES(3,4)

SELECT * FROM Rols
SELECT * FROM Permissions
SELECT * FROM Views
SELECT * FROM ViewsPermissions
SELECT * FROM RolsViewsPermissions
