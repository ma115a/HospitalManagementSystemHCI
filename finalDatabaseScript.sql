-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema hms
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema hms
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `hms` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `hms` ;

-- -----------------------------------------------------
-- Table `hms`.`employee`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`employee` (
  `employee_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(50) NOT NULL,
  `username` VARCHAR(20) NOT NULL,
  `email` VARCHAR(25) NOT NULL,
  `password` VARCHAR(300) NOT NULL,
  `phone` VARCHAR(20) NOT NULL,
  `date_employed` DATE NULL DEFAULT NULL,
  `active` TINYINT NULL DEFAULT NULL,
  `notes` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`employee_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 23
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`administrator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`administrator` (
  `employee_id` INT NOT NULL,
  PRIMARY KEY (`employee_id`),
  INDEX `fk_administrator_employee1_idx` (`employee_id` ASC) VISIBLE,
  CONSTRAINT `fk_administrator_employee1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `hms`.`employee` (`employee_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`patient`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`patient` (
  `name` VARCHAR(100) NULL DEFAULT NULL,
  `umcn` VARCHAR(13) NOT NULL,
  `phone` VARCHAR(20) NULL DEFAULT NULL,
  `notes` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`umcn`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`doctor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`doctor` (
  `employee_id` INT NOT NULL,
  `specialty` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_doctor_employee1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `hms`.`employee` (`employee_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`department`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`department` (
  `department_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NULL DEFAULT NULL,
  `doctor_employee_id` INT NOT NULL,
  `code` VARCHAR(45) NULL DEFAULT NULL,
  `surgery_department` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`department_id`),
  INDEX `fk_department_doctor1_idx` (`doctor_employee_id` ASC) VISIBLE,
  CONSTRAINT `fk_department_doctor1`
    FOREIGN KEY (`doctor_employee_id`)
    REFERENCES `hms`.`doctor` (`employee_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`room`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`room` (
  `room_id` INT NOT NULL AUTO_INCREMENT,
  `department_id` INT NOT NULL,
  `capacity` TINYINT(1) NULL DEFAULT NULL,
  `current_patients_number` INT NULL DEFAULT NULL,
  `number` INT NULL DEFAULT NULL,
  PRIMARY KEY (`room_id`),
  INDEX `fk_room_department2_idx` (`department_id` ASC) VISIBLE,
  CONSTRAINT `fk_room_department2`
    FOREIGN KEY (`department_id`)
    REFERENCES `hms`.`department` (`department_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`admission`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`admission` (
  `admission_id` INT NOT NULL AUTO_INCREMENT,
  `admission_date` DATE NULL DEFAULT NULL,
  `discharge_date` DATE NULL DEFAULT NULL,
  `reason` VARCHAR(200) NULL DEFAULT NULL,
  `patient_umcn` VARCHAR(13) NOT NULL,
  `room_id` INT NOT NULL,
  PRIMARY KEY (`admission_id`),
  INDEX `fk_admission_patient2_idx` (`patient_umcn` ASC) VISIBLE,
  INDEX `fk_admission_room2_idx` (`room_id` ASC) VISIBLE,
  CONSTRAINT `fk_admission_patient2`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`),
  CONSTRAINT `fk_admission_room2`
    FOREIGN KEY (`room_id`)
    REFERENCES `hms`.`room` (`room_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`appointment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`appointment` (
  `appointment_id` INT NOT NULL AUTO_INCREMENT,
  `doctor_id` INT NULL DEFAULT NULL,
  `date` DATETIME NULL DEFAULT NULL,
  `patient_umcn` VARCHAR(13) NOT NULL,
  `notes` LONGTEXT NULL DEFAULT NULL,
  `status` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`appointment_id`),
  INDEX `fk_appointment_doctor2_idx` (`doctor_id` ASC) VISIBLE,
  INDEX `fk_appointment_patient2_idx` (`patient_umcn` ASC) VISIBLE,
  CONSTRAINT `fk_appointment_doctor2`
    FOREIGN KEY (`doctor_id`)
    REFERENCES `hms`.`doctor` (`employee_id`),
  CONSTRAINT `fk_appointment_patient2`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`institution`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`institution` (
  `institution_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `address` VARCHAR(100) NOT NULL,
  `phone` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`institution_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`laboratory_tehnician`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`laboratory_tehnician` (
  `employee_id` INT NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_laboratory_tehnician_employee1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `hms`.`employee` (`employee_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`nurse`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`nurse` (
  `employee_id` INT NOT NULL,
  `specialty` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_nurse_employee1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `hms`.`employee` (`employee_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`laboratory_test`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`laboratory_test` (
  `laboratory_test_id` INT NOT NULL AUTO_INCREMENT,
  `laboratory_tehnician_id` INT NULL DEFAULT NULL,
  `doctor_id` INT NULL DEFAULT NULL,
  `patient_umcn` VARCHAR(13) NULL DEFAULT NULL,
  `description` VARCHAR(100) NULL DEFAULT NULL,
  `status` VARCHAR(45) NULL DEFAULT NULL,
  `name` VARCHAR(100) NULL DEFAULT NULL,
  `nurse_id` INT NULL DEFAULT NULL,
  `date` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`laboratory_test_id`),
  INDEX `fk_laboratory_test_laboratory_tehnician1_idx` (`laboratory_tehnician_id` ASC) VISIBLE,
  INDEX `fk_laboratory_test_doctor2_idx` (`doctor_id` ASC) VISIBLE,
  INDEX `fk_laboratory_test_patient2_idx` (`patient_umcn` ASC) VISIBLE,
  INDEX `fk_laboratory_test_nurse_idx` (`nurse_id` ASC) VISIBLE,
  CONSTRAINT `fk_laboratory_test_doctor2`
    FOREIGN KEY (`doctor_id`)
    REFERENCES `hms`.`doctor` (`employee_id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_laboratory_tehnician1`
    FOREIGN KEY (`laboratory_tehnician_id`)
    REFERENCES `hms`.`laboratory_tehnician` (`employee_id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_nurse`
    FOREIGN KEY (`nurse_id`)
    REFERENCES `hms`.`nurse` (`employee_id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_laboratory_test_patient2`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`)
    ON DELETE SET NULL
    ON UPDATE SET NULL)
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`laboratory_test_result`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`laboratory_test_result` (
  `laboratory_test_id` INT NOT NULL,
  `date` DATETIME NULL DEFAULT NULL,
  `result_data` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`laboratory_test_id`),
  CONSTRAINT `fk_laboratory_test_result_laboratory_test1`
    FOREIGN KEY (`laboratory_test_id`)
    REFERENCES `hms`.`laboratory_test` (`laboratory_test_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`medical_record`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`medical_record` (
  `medical_record_id` INT NOT NULL AUTO_INCREMENT,
  `doctor_id` INT NULL DEFAULT NULL,
  `date` DATETIME NULL DEFAULT NULL,
  `diagnosis` VARCHAR(200) NULL DEFAULT NULL,
  `treatment` VARCHAR(100) NULL DEFAULT NULL,
  `patient_umcn` VARCHAR(13) NOT NULL,
  `notes` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`medical_record_id`),
  INDEX `fk_medical_record_doctor1_idx` (`doctor_id` ASC) VISIBLE,
  INDEX `fk_medical_record_patient1` (`patient_umcn` ASC) VISIBLE,
  CONSTRAINT `fk_medical_record_doctor1`
    FOREIGN KEY (`doctor_id`)
    REFERENCES `hms`.`doctor` (`employee_id`),
  CONSTRAINT `fk_medical_record_patient1`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`prescription`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`prescription` (
  `prescription_id` INT NOT NULL AUTO_INCREMENT,
  `patient_umcn` VARCHAR(13) NULL DEFAULT NULL,
  `doctor_id` INT NULL DEFAULT NULL,
  `dosage` VARCHAR(50) NULL DEFAULT NULL,
  `frequency` VARCHAR(50) NULL DEFAULT NULL,
  `notes` VARCHAR(200) NULL DEFAULT NULL,
  `date` DATETIME NULL DEFAULT NULL,
  `duration` VARCHAR(45) NULL DEFAULT NULL,
  `refills` VARCHAR(45) NULL DEFAULT NULL,
  `medication` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`prescription_id`),
  INDEX `fk_prescription_patient2_idx` (`patient_umcn` ASC) VISIBLE,
  INDEX `fk_prescription_doctor2_idx` (`doctor_id` ASC) VISIBLE,
  CONSTRAINT `fk_prescription_doctor2`
    FOREIGN KEY (`doctor_id`)
    REFERENCES `hms`.`doctor` (`employee_id`),
  CONSTRAINT `fk_prescription_patient2`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`surgeon`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`surgeon` (
  `employee_id` INT NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `fk_surgeon_employee1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `hms`.`employee` (`employee_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`surgery`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`surgery` (
  `surgery_id` INT NOT NULL AUTO_INCREMENT,
  `patient_umcn` VARCHAR(13) NULL DEFAULT NULL,
  `surgeon_id` INT NULL DEFAULT NULL,
  `date` DATETIME NULL DEFAULT NULL,
  `room_id` INT NULL DEFAULT NULL,
  `procedure` VARCHAR(100) NULL DEFAULT NULL,
  `notes` LONGTEXT NULL DEFAULT NULL,
  `duration` INT NULL DEFAULT NULL,
  `status` VARCHAR(45) NULL DEFAULT NULL,
  `end_date` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`surgery_id`),
  INDEX `fk_surgery_patient2_idx` (`patient_umcn` ASC) VISIBLE,
  INDEX `fk_surgery_surgeon1_idx` (`surgeon_id` ASC) INVISIBLE,
  INDEX `fk_surgery_room_idx` (`room_id` ASC) INVISIBLE,
  CONSTRAINT `fk_surgery_patient2`
    FOREIGN KEY (`patient_umcn`)
    REFERENCES `hms`.`patient` (`umcn`),
  CONSTRAINT `fk_surgery_room`
    FOREIGN KEY (`room_id`)
    REFERENCES `hms`.`room` (`room_id`),
  CONSTRAINT `fk_surgery_surgeon1`
    FOREIGN KEY (`surgeon_id`)
    REFERENCES `hms`.`surgeon` (`employee_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 16
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`surgery_has_nurse`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`surgery_has_nurse` (
  `surgery_surgery_id` INT NOT NULL,
  `nurse_nurse_id` INT NULL DEFAULT NULL,
  INDEX `fk_surgery_has_nurse_nurse1_idx` (`nurse_nurse_id` ASC) VISIBLE,
  INDEX `fk_surgery_has_nurse_surgery1_idx` (`surgery_surgery_id` ASC) VISIBLE,
  CONSTRAINT `fk_surgery_has_nurse_nurse1`
    FOREIGN KEY (`nurse_nurse_id`)
    REFERENCES `hms`.`nurse` (`employee_id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_surgery_has_nurse_surgery1`
    FOREIGN KEY (`surgery_surgery_id`)
    REFERENCES `hms`.`surgery` (`surgery_id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `hms`.`vehicle`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hms`.`vehicle` (
  `vehicle_id` INT NOT NULL AUTO_INCREMENT,
  `brand` VARCHAR(20) NULL DEFAULT NULL,
  `model` VARCHAR(20) NULL DEFAULT NULL,
  `registration` VARCHAR(15) NULL DEFAULT NULL,
  `status` VARCHAR(45) NULL DEFAULT NULL,
  `notes` LONGTEXT NULL DEFAULT NULL,
  `last_service` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`vehicle_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
