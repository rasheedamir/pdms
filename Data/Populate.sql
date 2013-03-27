INSERT INTO groups (Id, Name, CreationTimestamp) VALUES (13, 'Group 1', '2013-03-22 12:02:30');
INSERT INTO groupstores (Id, GroupId, CoopStoreId) VALUES (13, 13, 111);
INSERT INTO packagegroups (Id, PackageVersionId, GroupId, ActivationTimestamp) VALUES (21, 23, 13, '2013-03-23 12:02:30');
INSERT INTO packagegroups (Id, PackageVersionId, GroupId, ActivationTimestamp) VALUES (22, 24, 13, '2013-04-04 12:02:30');
INSERT INTO packageversions (Id, VersionNumber, PackagePath, Comment, CreationTimestamp) VALUES (23, '1.0', 'C:\PDS\packages\packae_1.0.7z', 'This is package one.', '2013-03-22 12:02:30');
INSERT INTO packageversions (Id, VersionNumber, PackagePath, Comment, CreationTimestamp) VALUES (24, '2.0', 'C:\PDS\packages\packae_1.0.7z', 'This is package two.', '2013-03-24 16:09:33');
commit;