drop database pdms;
create database pdms;
use mysql;
grant all privileges on *.* to 'pdms'@'localhost' identified by 'pdms';
grant all privileges on *.* to 'pdms'@'%' identified by 'pdms';
commit;
use pdms;