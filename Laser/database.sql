-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: solder
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `almlisttable`
--

DROP TABLE IF EXISTS `almlisttable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `almlisttable` (
  `DateTime` datetime(6) NOT NULL,
  `number` int NOT NULL,
  `Message` varchar(500) NOT NULL,
  PRIMARY KEY (`DateTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `almlisttable`
--

LOCK TABLES `almlisttable` WRITE;
/*!40000 ALTER TABLE `almlisttable` DISABLE KEYS */;
INSERT INTO `almlisttable` VALUES ('2023-03-06 17:37:05.000000',11,'龙门2回零超时'),('2023-03-06 17:45:12.000000',13,'相机挡光板2伺服回零超时'),('2023-03-07 08:41:14.000000',13,'相机挡光板2伺服回零超时'),('2023-03-07 08:42:01.000000',14,'功率计伺服回零超时'),('2023-03-07 09:21:25.000000',15,'打齐11伺服回零超时'),('2023-03-07 09:21:45.000000',17,'打齐21伺服回零超时'),('2023-03-07 09:22:10.000000',14,'功率计伺服回零超时');
/*!40000 ALTER TABLE `almlisttable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dirtytable`
--

DROP TABLE IF EXISTS `dirtytable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dirtytable` (
  `id` varchar(255) NOT NULL,
  `GroupId` longtext NOT NULL,
  `SilicaId` longtext NOT NULL,
  `MachineId` longtext NOT NULL,
  `WorkStationId` int NOT NULL,
  `LaserId` int NOT NULL,
  `IsDirty` longtext NOT NULL,
  `DirtyX` double NOT NULL,
  `DirtyY` double NOT NULL,
  `DirtyWidth` double NOT NULL,
  `DirtyHeight` double NOT NULL,
  `PadX` double NOT NULL,
  `PadY` double NOT NULL,
  `Time` datetime(6) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dirtytable`
--

LOCK TABLES `dirtytable` WRITE;
/*!40000 ALTER TABLE `dirtytable` DISABLE KEYS */;
/*!40000 ALTER TABLE `dirtytable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productdefecttable`
--

DROP TABLE IF EXISTS `productdefecttable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productdefecttable` (
  `id` varchar(255) NOT NULL,
  `GroupId` longtext NOT NULL,
  `SilicaId` longtext NOT NULL,
  `WorkStationId` int NOT NULL,
  `LaserId` int NOT NULL,
  `PadX` double NOT NULL,
  `PadY` double NOT NULL,
  `Time` datetime(6) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productdefecttable`
--

LOCK TABLES `productdefecttable` WRITE;
/*!40000 ALTER TABLE `productdefecttable` DISABLE KEYS */;
/*!40000 ALTER TABLE `productdefecttable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userlogintable`
--

DROP TABLE IF EXISTS `userlogintable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userlogintable` (
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `DebugLimit` tinyint(1) DEFAULT '0',
  `ParamSetLimit` tinyint(1) DEFAULT '0',
  `MarkingLimit` tinyint(1) DEFAULT '0',
  `PhotoLimit` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userlogintable`
--

LOCK TABLES `userlogintable` WRITE;
/*!40000 ALTER TABLE `userlogintable` DISABLE KEYS */;
INSERT INTO `userlogintable` VALUES ('Administrator','123',1,1,1,1),('eret','12',1,1,0,0),('errhd','dsfs',0,0,0,0),('fdh','12',1,1,1,0);
/*!40000 ALTER TABLE `userlogintable` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-10 13:59:16
