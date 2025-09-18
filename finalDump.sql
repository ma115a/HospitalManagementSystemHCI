-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: hms
-- ------------------------------------------------------
-- Server version	8.0.36

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
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrator` (
  `employee_id` int NOT NULL,
  PRIMARY KEY (`employee_id`),
  KEY `fk_administrator_employee1_idx` (`employee_id`),
  CONSTRAINT `fk_administrator_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
INSERT INTO `administrator` VALUES (23);
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admission`
--

DROP TABLE IF EXISTS `admission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admission` (
  `admission_id` int NOT NULL AUTO_INCREMENT,
  `admission_date` date DEFAULT NULL,
  `discharge_date` date DEFAULT NULL,
  `reason` varchar(200) DEFAULT NULL,
  `patient_umcn` varchar(13) NOT NULL,
  `room_id` int NOT NULL,
  PRIMARY KEY (`admission_id`),
  KEY `fk_admission_patient2_idx` (`patient_umcn`),
  KEY `fk_admission_room2_idx` (`room_id`),
  CONSTRAINT `fk_admission_patient2` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`),
  CONSTRAINT `fk_admission_room2` FOREIGN KEY (`room_id`) REFERENCES `room` (`room_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admission`
--

LOCK TABLES `admission` WRITE;
/*!40000 ALTER TABLE `admission` DISABLE KEYS */;
INSERT INTO `admission` VALUES (7,'2025-09-19','2025-09-26',NULL,'4444444444444',6),(8,'2025-09-10','2025-09-17',NULL,'5555555555555',7);
/*!40000 ALTER TABLE `admission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointment`
--

DROP TABLE IF EXISTS `appointment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment` (
  `appointment_id` int NOT NULL AUTO_INCREMENT,
  `doctor_id` int DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `patient_umcn` varchar(13) NOT NULL,
  `notes` longtext,
  `status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`appointment_id`),
  KEY `fk_appointment_doctor2_idx` (`doctor_id`),
  KEY `fk_appointment_patient2_idx` (`patient_umcn`),
  CONSTRAINT `fk_appointment_doctor2` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`employee_id`),
  CONSTRAINT `fk_appointment_patient2` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment`
--

LOCK TABLES `appointment` WRITE;
/*!40000 ALTER TABLE `appointment` DISABLE KEYS */;
INSERT INTO `appointment` VALUES (6,25,'2025-09-18 10:00:00','3333333333333','notes','Scheduled'),(7,24,'2025-09-20 10:00:00','1111111111111','notes','Scheduled'),(8,25,'2025-09-20 12:00:00','5555555555555','notes','Scheduled'),(9,25,'2025-09-30 13:00:00','1111111111111',NULL,'Scheduled');
/*!40000 ALTER TABLE `appointment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department` (
  `department_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `doctor_employee_id` int NOT NULL,
  `code` varchar(45) DEFAULT NULL,
  `surgery_department` tinyint DEFAULT NULL,
  PRIMARY KEY (`department_id`),
  KEY `fk_department_doctor1_idx` (`doctor_employee_id`),
  CONSTRAINT `fk_department_doctor1` FOREIGN KEY (`doctor_employee_id`) REFERENCES `doctor` (`employee_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (4,'Department 1',24,'112',NULL),(5,'Department  2',25,'113',NULL),(8,'Surgery department',25,'114',1);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor`
--

DROP TABLE IF EXISTS `doctor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor` (
  `employee_id` int NOT NULL,
  `specialty` varchar(45) NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_doctor_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor`
--

LOCK TABLES `doctor` WRITE;
/*!40000 ALTER TABLE `doctor` DISABLE KEYS */;
INSERT INTO `doctor` VALUES (24,'Cardiology'),(25,'Pediatrics');
/*!40000 ALTER TABLE `doctor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `employee_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `username` varchar(20) NOT NULL,
  `email` varchar(25) NOT NULL,
  `password` varchar(300) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `date_employed` date DEFAULT NULL,
  `active` tinyint DEFAULT NULL,
  `notes` longtext,
  PRIMARY KEY (`employee_id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (23,'Mihajlo Vukajlovic','admin','a@a.com','jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=','066478223','2025-09-18',1,'some notes'),(24,'Senad Kazibegic','doctor1','a@a.com','Xwqcf2JK/Hl5jmbX/oDC8LhVU+LWY/ur1F1MR2ReebA=','066478223','2025-09-18',1,''),(25,'Adem Nezic','doctor2','a@a.com','qLRmpd3us3lVbzAkPTvLc3fMYhhgw+ypX7GMI96f79k=','066478223','2025-09-18',1,''),(26,'Marko Petrovic','surgeon1','a@a.com','3jJLq6wDFUnkxXs4OdoYDjLzsJ023DiM3ggsN1S81/o=','066478223','2025-09-18',1,''),(27,'Luka Stankovic','surgeon2','a@a.com','xdEY9FDFE934GgUDGdG546SoEXnoxy5zcx5XyQvi6zo=','066478223','2025-09-18',1,''),(28,'Ana Petrovic','nurse1','a@a.com','5ZTnCiy9CJN7uHwQO4ZOxntXajMdqdfBBl5a5FHNEmU=','066478223','2025-09-18',1,''),(29,'Milica Jovanovic','nurse2','a@a.com','YNT335VqMMHqOr7z2lF5j46CynMhmJhCPm2QLNYljqY=','066478223','2025-09-18',1,''),(30,'Teodora Nikolic','nurse3','a@a.com','KYcB6zhNZNo5GPNAWglek45HyO7ZRnFr4vCRy+IGiTw=','066478223','2025-09-18',1,''),(31,'Jelena Pavlovic','nurse4','a@a.com','zwNrsSRiU/fG0+JkgIU6jU6G9mnXVTCmabjapmokrM0=','066478223','2025-09-18',1,''),(32,'Natasa Radovic','lab1','a@a.com','aNCgP71ARIm5h+fheuUXsrAlC+4OcZ0EONfkESn3Zgk=','066478223','2025-09-18',1,''),(33,'Isidora Lazarevic','lab2','a@a.com','d4EucMnG16C329gjPzy+IT9xrA7s0eYYSuWBYJXauaI=','066478223','2025-09-18',1,'');
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `institution`
--

DROP TABLE IF EXISTS `institution`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `institution` (
  `institution_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `address` varchar(100) NOT NULL,
  `phone` varchar(20) NOT NULL,
  PRIMARY KEY (`institution_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `institution`
--

LOCK TABLES `institution` WRITE;
/*!40000 ALTER TABLE `institution` DISABLE KEYS */;
/*!40000 ALTER TABLE `institution` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `laboratory_tehnician`
--

DROP TABLE IF EXISTS `laboratory_tehnician`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `laboratory_tehnician` (
  `employee_id` int NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_laboratory_tehnician_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `laboratory_tehnician`
--

LOCK TABLES `laboratory_tehnician` WRITE;
/*!40000 ALTER TABLE `laboratory_tehnician` DISABLE KEYS */;
INSERT INTO `laboratory_tehnician` VALUES (32),(33);
/*!40000 ALTER TABLE `laboratory_tehnician` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `laboratory_test`
--

DROP TABLE IF EXISTS `laboratory_test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `laboratory_test` (
  `laboratory_test_id` int NOT NULL AUTO_INCREMENT,
  `laboratory_tehnician_id` int DEFAULT NULL,
  `doctor_id` int DEFAULT NULL,
  `patient_umcn` varchar(13) DEFAULT NULL,
  `description` varchar(100) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `nurse_id` int DEFAULT NULL,
  `date` date DEFAULT NULL,
  PRIMARY KEY (`laboratory_test_id`),
  KEY `fk_laboratory_test_laboratory_tehnician1_idx` (`laboratory_tehnician_id`),
  KEY `fk_laboratory_test_doctor2_idx` (`doctor_id`),
  KEY `fk_laboratory_test_patient2_idx` (`patient_umcn`),
  KEY `fk_laboratory_test_nurse_idx` (`nurse_id`),
  CONSTRAINT `fk_laboratory_test_doctor2` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`employee_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_laboratory_tehnician1` FOREIGN KEY (`laboratory_tehnician_id`) REFERENCES `laboratory_tehnician` (`employee_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_nurse` FOREIGN KEY (`nurse_id`) REFERENCES `nurse` (`employee_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_patient2` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `laboratory_test`
--

LOCK TABLES `laboratory_test` WRITE;
/*!40000 ALTER TABLE `laboratory_test` DISABLE KEYS */;
INSERT INTO `laboratory_test` VALUES (9,33,NULL,'4444444444444','reason ','Completed','test1',28,'2025-09-18'),(10,33,NULL,'2222222222222','reason2','Requested','test2',28,'2025-09-18');
/*!40000 ALTER TABLE `laboratory_test` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `laboratory_test_result`
--

DROP TABLE IF EXISTS `laboratory_test_result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `laboratory_test_result` (
  `laboratory_test_id` int NOT NULL,
  `date` datetime DEFAULT NULL,
  `result_data` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`laboratory_test_id`),
  CONSTRAINT `fk_laboratory_test_result_laboratory_test1` FOREIGN KEY (`laboratory_test_id`) REFERENCES `laboratory_test` (`laboratory_test_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `laboratory_test_result`
--

LOCK TABLES `laboratory_test_result` WRITE;
/*!40000 ALTER TABLE `laboratory_test_result` DISABLE KEYS */;
INSERT INTO `laboratory_test_result` VALUES (9,'2025-09-18 06:58:00','gotov test');
/*!40000 ALTER TABLE `laboratory_test_result` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medical_record`
--

DROP TABLE IF EXISTS `medical_record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medical_record` (
  `medical_record_id` int NOT NULL AUTO_INCREMENT,
  `doctor_id` int DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `diagnosis` varchar(200) DEFAULT NULL,
  `treatment` varchar(100) DEFAULT NULL,
  `patient_umcn` varchar(13) NOT NULL,
  `notes` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`medical_record_id`),
  KEY `fk_medical_record_doctor1_idx` (`doctor_id`),
  KEY `fk_medical_record_patient1` (`patient_umcn`),
  CONSTRAINT `fk_medical_record_doctor1` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`employee_id`),
  CONSTRAINT `fk_medical_record_patient1` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medical_record`
--

LOCK TABLES `medical_record` WRITE;
/*!40000 ALTER TABLE `medical_record` DISABLE KEYS */;
INSERT INTO `medical_record` VALUES (3,24,'2025-09-18 10:00:00','diagnosis edited','ua ua','5555555555555','note'),(4,24,'2025-09-19 09:00:00','diagnosis','asdf','4444444444444','note');
/*!40000 ALTER TABLE `medical_record` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nurse`
--

DROP TABLE IF EXISTS `nurse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nurse` (
  `employee_id` int NOT NULL,
  `specialty` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_nurse_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nurse`
--

LOCK TABLES `nurse` WRITE;
/*!40000 ALTER TABLE `nurse` DISABLE KEYS */;
INSERT INTO `nurse` VALUES (28,'Pediatrics'),(29,'Orthopedics'),(30,'Cardiology'),(31,'Neurology');
/*!40000 ALTER TABLE `nurse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient` (
  `name` varchar(100) DEFAULT NULL,
  `umcn` varchar(13) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `notes` longtext,
  PRIMARY KEY (`umcn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
INSERT INTO `patient` VALUES ('Stefan Jovanovic','1111111111111','066478223',NULL),('Milos Markovic','2222222222222','066478223',NULL),('Vuk Lazarevic','3333333333333','066478223',NULL),('Jelena Pavlovic','4444444444444','066478223',NULL),('Natasa Radovic','5555555555555','066478223',NULL);
/*!40000 ALTER TABLE `patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription`
--

DROP TABLE IF EXISTS `prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription` (
  `prescription_id` int NOT NULL AUTO_INCREMENT,
  `patient_umcn` varchar(13) DEFAULT NULL,
  `doctor_id` int DEFAULT NULL,
  `dosage` varchar(50) DEFAULT NULL,
  `frequency` varchar(50) DEFAULT NULL,
  `notes` varchar(200) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `duration` varchar(45) DEFAULT NULL,
  `refills` varchar(45) DEFAULT NULL,
  `medication` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`prescription_id`),
  KEY `fk_prescription_patient2_idx` (`patient_umcn`),
  KEY `fk_prescription_doctor2_idx` (`doctor_id`),
  CONSTRAINT `fk_prescription_doctor2` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`employee_id`),
  CONSTRAINT `fk_prescription_patient2` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
INSERT INTO `prescription` VALUES (9,'4444444444444',NULL,'300mg','Every 4 hours','note','2025-09-19 00:00:00','7','2','Atorvastatin');
/*!40000 ALTER TABLE `prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room`
--

DROP TABLE IF EXISTS `room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room` (
  `room_id` int NOT NULL AUTO_INCREMENT,
  `department_id` int NOT NULL,
  `capacity` tinyint(1) DEFAULT NULL,
  `current_patients_number` int DEFAULT '0',
  `number` int DEFAULT NULL,
  PRIMARY KEY (`room_id`),
  KEY `fk_room_department2_idx` (`department_id`),
  CONSTRAINT `fk_room_department2` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room`
--

LOCK TABLES `room` WRITE;
/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room` VALUES (6,4,2,0,1),(7,4,2,0,2),(8,4,3,0,3),(9,5,2,0,1),(10,5,2,0,2),(11,4,3,0,3),(12,8,1,0,1),(13,8,1,0,2),(14,8,1,0,3);
/*!40000 ALTER TABLE `room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgeon`
--

DROP TABLE IF EXISTS `surgeon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgeon` (
  `employee_id` int NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_surgeon_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`employee_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgeon`
--

LOCK TABLES `surgeon` WRITE;
/*!40000 ALTER TABLE `surgeon` DISABLE KEYS */;
INSERT INTO `surgeon` VALUES (26),(27);
/*!40000 ALTER TABLE `surgeon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgery`
--

DROP TABLE IF EXISTS `surgery`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgery` (
  `surgery_id` int NOT NULL AUTO_INCREMENT,
  `patient_umcn` varchar(13) DEFAULT NULL,
  `surgeon_id` int DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `room_id` int DEFAULT NULL,
  `procedure` varchar(100) DEFAULT NULL,
  `notes` longtext,
  `duration` int DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `end_date` datetime DEFAULT NULL,
  PRIMARY KEY (`surgery_id`),
  KEY `fk_surgery_patient2_idx` (`patient_umcn`),
  KEY `fk_surgery_surgeon1_idx` (`surgeon_id`) /*!80000 INVISIBLE */,
  KEY `fk_surgery_room_idx` (`room_id`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_surgery_patient2` FOREIGN KEY (`patient_umcn`) REFERENCES `patient` (`umcn`),
  CONSTRAINT `fk_surgery_room` FOREIGN KEY (`room_id`) REFERENCES `room` (`room_id`),
  CONSTRAINT `fk_surgery_surgeon1` FOREIGN KEY (`surgeon_id`) REFERENCES `surgeon` (`employee_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgery`
--

LOCK TABLES `surgery` WRITE;
/*!40000 ALTER TABLE `surgery` DISABLE KEYS */;
INSERT INTO `surgery` VALUES (16,'5555555555555',27,'2025-09-26 09:00:00',12,'neka procedura','biljeska',30,'Scheduled','2025-09-26 09:30:00');
/*!40000 ALTER TABLE `surgery` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgery_has_nurse`
--

DROP TABLE IF EXISTS `surgery_has_nurse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgery_has_nurse` (
  `surgery_surgery_id` int NOT NULL,
  `nurse_nurse_id` int DEFAULT NULL,
  KEY `fk_surgery_has_nurse_nurse1_idx` (`nurse_nurse_id`),
  KEY `fk_surgery_has_nurse_surgery1_idx` (`surgery_surgery_id`),
  CONSTRAINT `fk_surgery_has_nurse_nurse1` FOREIGN KEY (`nurse_nurse_id`) REFERENCES `nurse` (`employee_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_surgery_has_nurse_surgery1` FOREIGN KEY (`surgery_surgery_id`) REFERENCES `surgery` (`surgery_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgery_has_nurse`
--

LOCK TABLES `surgery_has_nurse` WRITE;
/*!40000 ALTER TABLE `surgery_has_nurse` DISABLE KEYS */;
INSERT INTO `surgery_has_nurse` VALUES (16,29),(16,30);
/*!40000 ALTER TABLE `surgery_has_nurse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicle`
--

DROP TABLE IF EXISTS `vehicle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicle` (
  `vehicle_id` int NOT NULL AUTO_INCREMENT,
  `brand` varchar(20) DEFAULT NULL,
  `model` varchar(20) DEFAULT NULL,
  `registration` varchar(15) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `notes` longtext,
  `last_service` date DEFAULT NULL,
  PRIMARY KEY (`vehicle_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle`
--

LOCK TABLES `vehicle` WRITE;
/*!40000 ALTER TABLE `vehicle` DISABLE KEYS */;
INSERT INTO `vehicle` VALUES (1,'Sprinter','Mercedes','KLM01','Operational','',NULL);
/*!40000 ALTER TABLE `vehicle` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-18 21:31:02
