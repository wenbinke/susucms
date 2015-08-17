SET IDENTITY_INSERT [Cms_AdminRole] On
INSERT INTO [Cms_AdminRole]
           ([Id]
           ,[Name]
           ,[Permissions]
           ,[IsRoot])
     VALUES
           (1
           ,'Administrator'
           ,0xFFFFFFFFFFFFFFFFFFFFFFFF
           ,1)
GO
SET IDENTITY_INSERT [Cms_AdminRole] Off


SET IDENTITY_INSERT [Cms_AdminUser] On

INSERT INTO [Cms_AdminUser]
           ([Id]
           ,[UserName]
           ,[Salt]
           ,[Password]
           ,[Email]
           ,[CreationTime]
           ,[Status]
           ,[RoleId]
           ,[IsRoot])
     VALUES
           (1
           ,'admin'
           ,'VcfNRDk8Wq0MgvCpkT8wjA=='
           ,'AF0GyABm9/IGni6K3r0yUdhpC8Ae8EXLdvo7wMrAzISI87IJP70C2cYNCzjjUMSdVQ=='
           ,'admin@admin.com'
           ,getdate()
           ,0
           ,1
           ,1)
GO

SET IDENTITY_INSERT [Cms_AdminUser] Off