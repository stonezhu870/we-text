CREATE DATABASE `wetext.accounts` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `wetext.accounts`;
CREATE TABLE `accounts` (
  `Id` varchar(64) NOT NULL,
  `Name` varchar(16) NOT NULL,
  `Email` varchar(32) NOT NULL,
  `Password` varchar(16) NOT NULL,
  `DisplayName` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `appinfo` (
  `AppKey` varchar(36) NOT NULL,
  `AppSecret` varchar(32) NOT NULL,
  `Title` varchar(50) NOT NULL,
  `Remark` varchar(200) NOT NULL,
  `Icon` varchar(500) DEFAULT NULL,
  `ReturnUrl` varchar(1024) DEFAULT NULL,
  `IsEnable` bit DEFAULT false,
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`AppKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE DATABASE `wetext.social` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `wetext.social`;
CREATE TABLE `networks` (
  `OriginatorId` varchar(64) NOT NULL,
  `TargetId` varchar(64) NOT NULL,
  `InvitationStartDate` datetime NOT NULL,
  `InvitationEndDate` datetime DEFAULT NULL,
  `InvitationEndReason` int(11) DEFAULT NULL,
  `InvitationId` varchar(64) NOT NULL,
  `OriginatorName` varchar(32) NOT NULL,
  `TargetUserName` varchar(32) NOT NULL,
  PRIMARY KEY (`InvitationId`),
  UNIQUE KEY `InvitationId_UNIQUE` (`InvitationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
CREATE TABLE `usernames` (
  `UserId` varchar(64) NOT NULL,
  `DisplayName` varchar(32) NOT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `UserId_UNIQUE` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE DATABASE `wetext.texting` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `wetext.texting`;
CREATE TABLE `texts` (
  `Id` varchar(64) NOT NULL,
  `UserId` varchar(64) NOT NULL,
  `Title` varchar(32) NOT NULL,
  `Content` longtext NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

