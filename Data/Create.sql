CREATE TABLE groups (Id int NOT NULL AUTO_INCREMENT, Name varchar(100), CreationTimestamp datetime, PRIMARY KEY (Id)) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE groupstores (Id int NOT NULL AUTO_INCREMENT, GroupId int, CoopStoreId int, PRIMARY KEY (Id), INDEX GroupId_idx (GroupId)) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE packagegroups (Id int NOT NULL AUTO_INCREMENT, PackageVersionId int, GroupId int, ActivationTimestamp datetime, PRIMARY KEY (Id), INDEX PackageVersionId_idx (PackageVersionId), INDEX GroupId_idx (GroupId)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Provides mapping of packages to groups.';

CREATE TABLE packageversions (Id int NOT NULL AUTO_INCREMENT, VersionNumber varchar(50), PackagePath varchar(75) COMMENT 'It should have complete path along with the package name.', Comment varchar(250), CreationTimestamp datetime, PRIMARY KEY (Id)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Contains the list of packages.';

CREATE TABLE posassemblylogs (Id int NOT NULL AUTO_INCREMENT, PosCallLogId int, AssemblyName varchar(100), AssemblyVersion varchar(50), PRIMARY KEY (Id), INDEX PosCallLogId_idx (PosCallLogId)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Contains log of the pos assemblies.';

CREATE TABLE poscalllogs (Id int NOT NULL AUTO_INCREMENT, CoopStoreId int, PosNumber varchar(100), CallTimestamp datetime, PackageVersionId int, TerminalSerialNumber varchar(100), IpAddress varchar(25), HostName varchar(100), PosManufacturerName varchar(100), PosVersion varchar(100), PRIMARY KEY (Id), INDEX PackageVersionId_idx (PackageVersionId)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Contains the log of the calls from pos (point of sales) systems.';

ALTER TABLE groupstores ADD CONSTRAINT GS_GroupId_FK FOREIGN KEY (GroupId) REFERENCES groups (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE packagegroups ADD CONSTRAINT PG_GroupId_FK FOREIGN KEY (GroupId) REFERENCES groups (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE packagegroups ADD CONSTRAINT PG_PackageVersionId_FK FOREIGN KEY (PackageVersionId) REFERENCES packageversions (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE posassemblylogs ADD CONSTRAINT PAL_PosCallLogId_FK FOREIGN KEY (PosCallLogId) REFERENCES poscalllogs (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE poscalllogs ADD CONSTRAINT PCL_PackageVersionId_FK FOREIGN KEY (PackageVersionId) REFERENCES packageversions (Id) ON DELETE NO ACTION ON UPDATE NO ACTION;
commit;