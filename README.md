# 🏥 Hospital Management System (HMS)

The **Hospital Management System (HMS)** is a comprehensive software solution designed to manage hospital operations, improve efficiency, and ensure seamless coordination between healthcare staff, patients, and administration.  

It provides functionality to manage patients, doctors, nurses, medical records, appointments, admissions, surgeries, laboratory tests, prescriptions, vehicles, drivers, and more.  

---

## 📌 Motivation

Managing a hospital involves handling large volumes of data and ensuring accuracy, efficiency, and accessibility. Traditional systems often rely on paper-based or fragmented digital solutions, which can lead to inefficiencies and errors.  

The motivation behind this project is to:
- Simplify hospital workflows by centralizing data.  
- Enable healthcare professionals to quickly access patient and institutional records.  
- Improve patient care by streamlining appointments, admissions, and prescriptions.  
- Provide administrators with better control over hospital resources (staff, rooms, vehicles, medications).  

---

## 🗄️ Database Schema

The system is backed by a **MySQL database schema** (`create_hms.sql`) that defines core entities such as:

- **Institution**: Hospital or clinic details.  
- **Employee**: Doctors, nurses, surgeons, laboratory technicians.  
- **Patient**: Patient records with UMCN as a unique identifier.  
- **Appointments & Admissions**: Scheduling and hospital stays.  
- **Medical Records & Prescriptions**: Treatment history and medication tracking.  
- **Laboratory Tests & Results**: Diagnostic management.  
- **Vehicles & Drivers**: For hospital logistics and emergency needs.  
- **Administration**: Administrators and their actions.  

---

## ⚙️ Installation & Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/hospital-management-system.git
   cd hospital-management-system
