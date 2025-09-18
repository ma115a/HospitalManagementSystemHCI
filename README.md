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
- Provide administrators with better control over hospital resources (staff, rooms, vehicles).  

---

## 🗄️ Database Schema

The system is backed by a **MySQL database schema** (`finalDatabaseScript.sql`) and (`finalDatabaseDump.sql`) that defines core entities such as:

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
   cd hospital-management-system```
2. **Set up the database**:
   - Open MySQL Workbench and run the two scripts for creating the schema, and populating the database.
3. **Configure application**
   - Update your connection string(username, password, database) in the appsettings.json
   - Build and run the application

## 📖 User manual
### For Doctors
- **View and Update Medical Records**
- **View and Make new Prescriptions**
- **Request Laboratory tests and track their status**
- **Make admissions**
- **Request and view surgeries**
  
  <img width="1231" height="951" alt="image" src="https://github.com/user-attachments/assets/feebfe0a-e82f-4c44-8190-8aa801312a0b" />
Each functionality is presented by clicking the "Open" button
  **View and Update Medical Records**
  
  <img width="1229" height="953" alt="image" src="https://github.com/user-attachments/assets/a60f1c2f-e8b3-47ee-ad3b-4ce9072bbfde" />
  
  On the right side you can see all the records associated with the selected patient.
  By clicking on the "New" button you can create a new record and save it to the database.
  By clicking "Edit" or "Delete", you can either edit the selected recordd or delete it permanently. Also clicking on one of the records from the grid, you can see all the details it contains.
  By filling out the filter fields at the top you can filter the records.
  By clicking the "Back to Dashboard" button you can go back to the home page.

  **View and Make new Prescriptions**
  <img width="1419" height="893" alt="image" src="https://github.com/user-attachments/assets/b7ecb1a6-e9c9-4c02-8027-cf77e03b1973" />
  
  On the right side you can see all the prescriptions associated with the selected patient.
  By clicking the "New" button you can create a new prescription, fill out the form, and click "Save" and it will be saved to the database. You can abort that process by clicking the "Cancel" button.
  By clicking the "Edit" button you can edit the selected prescription, and click on "Save" to save it permanently.
  There is also a filter form at the top, so filtering by medication name and date is available. By clicking the "Cancel" button next to the filters, it clears the filters and all of the prescriptions are shown again.
  Clicking on the "Back to Dashboard" button you can navigate back to the home page.  

  **Request Laboratory and track their status**
<img width="1351" height="928" alt="image" src="https://github.com/user-attachments/assets/a87e4635-435a-4a0f-803e-e99694d7ae0b" />

Here you can see all the laboratory test requests and results associated with the selected patient.
The view is split in two tabs, one for the requests and one for the results.
You can create a new request by clicking on the "New" button at the top, fill out the form and save the request, and it will later show up to the laboratory worker as requested. 
You can also edit or delete the request by clicking the appropriate buttons after selecting a request.
Filtering the requests is also possible by their name, and date.  

Clicking on "Results" tab shows you the following screen:
<img width="1296" height="957" alt="image" src="https://github.com/user-attachments/assets/b2488919-c06b-445f-85aa-da9f6311ab4c" />

Here you can see all the tests that have been completed by the laboratory workers, select one and see it's details.  

**Make Admissions**

<img width="1300" height="852" alt="image" src="https://github.com/user-attachments/assets/ddcceeb1-6b92-4d1c-973e-82007a3f586c" />
  
From this screen you can admit a patient to one of the rooms.
By default all the rooms from all departments are shown.
To select a room, you first select a department the room belongs to, and then the available rooms in that department show up.
By selecting a room you can fill out the form and make the admission.
Editing or deleting the selected admission is possible by clicking the appropriate buttons.  

**Request and View Surgeries**

<img width="1297" height="959" alt="image" src="https://github.com/user-attachments/assets/a4214130-bbd5-4815-b3c9-3022606c3a05" />

Via this view,, you can see the list of surgeries the selected patient had.
To schedule a new surgery you click on the "New" button and fill out the form.
Keep in mind, when you fill out the date, time and duration of the surgery, then the nurses that are available load. Clicking on the pick nurses yields you this little pop-up
<img width="781" height="497" alt="image" src="https://github.com/user-attachments/assets/8bfada85-4bb5-4572-a2bf-58caf2d10883" />
  
Here you can filter the nurses by their name, and checking the little box doesnt automatically add them to the surgery, you need to click the save button, if you click the cancel button the selected nurses will not be added.


### For Nurses  
- **Manage and Schedule Appointments**
- **Make Admissions**
- **Request Laboratory tests and track their status**
- **Register patients**

  By logging in as a nurse you are shown the following screen that represents the homepage.

  <img width="1296" height="949" alt="image" src="https://github.com/user-attachments/assets/16064543-e379-4814-bd08-aab97ff3dbf5" />



  **Manage and Schedule Appointments**
  <img width="1291" height="953" alt="image" src="https://github.com/user-attachments/assets/bcda85e6-9861-4862-9184-890886e419ee" />
  Here you can see all the appointments.
  By selecting an appointment you can edit it or delete it, change it's status etc. You can also filter them by date, or by the doctor.
  Clickin on the "New" button, the following screen is shown
  <img width="1296" height="950" alt="image" src="https://github.com/user-attachments/assets/d2ecd33d-4707-49b4-960a-866777bf7fbc" />
  Filling out this form schedules a new appointment for the selected patient.
  This form is also available directly from the home page for ease of access.

  **Make Admission**
  <img width="1294" height="849" alt="image" src="https://github.com/user-attachments/assets/9827caee-b32a-4d98-ad4a-5ce3c13361a2" />

  Doctor and nurse share the same form so everything stated for the doctor is applicable here.

  **Request Laboratory tests and track their status**
  <img width="1296" height="849" alt="image" src="https://github.com/user-attachments/assets/f83d4f67-611f-4fe4-9f83-838a6849aafa" />

  Doctor and nurse share the same form so everything stated for the doctor is applicable here.
  In this form, all the laboratory tests are show, but filtering them by patient is possible.

  **Register patient**

  <img width="1296" height="849" alt="image" src="https://github.com/user-attachments/assets/7bbbce85-88eb-4d0c-a0ee-ce8b80d9889b" />

  Here you can see all the registered patients in the system, add new ones, edit and delete existing.
  All the same principals value here, "New" button enables the form for creating a new patient, "Edit" enables the form for editing the selected patient and "Delete" deletes the patient from the system.

  

### For Laboratory Technicians  
- **See requests queue**
- **Enter results**
- **See History**

  By logging in as a laboratory technician you are shown the following screen.
  <img width="1296" height="701" alt="image" src="https://github.com/user-attachments/assets/1a44ee75-8f41-4120-b9a9-f0467edc7211" />

  **See requests queue**
  <img width="1296" height="701" alt="image" src="https://github.com/user-attachments/assets/dc5ec8a4-0b83-4b44-a390-9022987480a4" />

  Here you can see all the pending laboratory requests that need to be completed
  You can filter the tests by date, patient and the nurse or the doctor that requested the laboratory.
  By selecting a test and clicking on "Start test" you change the state of the test to "In progress" from "Requested"
  This is crucial for completing the test, without this you will not be able to enter results for the selected test.

  **Enter results**
  <img width="1294" height="825" alt="image" src="https://github.com/user-attachments/assets/a917ea97-6057-448c-981e-8535c20d1191" />
  Here you can enter results for a test you started. If multiple tests are started you can select the test from the drop menu.
  Filling out the form with the relevatnt data, saves the test result.

  **See History**
  <img width="1294" height="728" alt="image" src="https://github.com/user-attachments/assets/ba2e0ab7-29f0-4129-87ea-2e1d66662010" />

  This screen is very simillar to the requests one, with one difference beiing the right side where you can see the test result for the selected test.

  ### For Surgeons
- **See surgeries**
- **Schedule surgery**

   By logging in as a surgeon you are presented by the following screen.  
  <img width="1357" height="952" alt="image" src="https://github.com/user-attachments/assets/32629ca5-1df5-4806-a3d1-91ed688da846" />

  **See surgeries**
  <img width="1353" height="854" alt="image" src="https://github.com/user-attachments/assets/dadd7924-af7c-45ec-9fc2-92e090327d5a" />

  From here you can see the list of the associated surgeries, schedule new ones, edit or delete the selected one.
  On the left side there is a grid with all the surgeries, and on the right there is a tabbed form.
  In the Details form you can see all the details about the surgery, and on the Team tab you can see all the assigned nurses for the surgery, and edit them. That means add new ones or remove the existing.

  **Schedule surgery**
  <img width="1358" height="846" alt="image" src="https://github.com/user-attachments/assets/d4548164-3176-4309-8923-6566d3f14b82" />

  Doctor and surgeon share the same view/form so all the things said about it in the doctors section are applicable here

    ### For Admins
- **Manage User Profiles**
- **Manage Vehicles**
- **Manage Departments and Rooms**

  By logging in as an administrator you are presented with the following view.
  <img width="1229" height="942" alt="image" src="https://github.com/user-attachments/assets/29d847c0-bd2b-4382-81bf-491ac6b90131" />

  **Manage User Profiles**
  <img width="1232" height="850" alt="image" src="https://github.com/user-attachments/assets/8239b28b-9fa2-4ca2-be58-a2e03076b32b" />
  From here you can add new profiles for all types of users, edit or delete them.
  On the left side all of the employees are show.
  The standard approach for creation is applicable here, by clicking the "New" buttton.

  **Manage Vehicles**
  <img width="1228" height="848" alt="image" src="https://github.com/user-attachments/assets/208ff928-df51-456e-87cb-e7f476dc800c" />  
  Here on the left side, all of the registered vehicles are show, and on the right side, a form for creating new ones is shown.
  To create a new vehicle, you click on the "New" button, to edit or delete a vehicle, you first have to select one and then click on the appropriate button.

  **Manage Departments and Rooms**
  <img width="1229" height="849" alt="image" src="https://github.com/user-attachments/assets/021df0ea-8a19-4960-9929-b541ba6c19d9" />  
  By default on the left side all of the departments are shown. To view rooms, you first need to select the department and then on the right all of the rooms will be shown for that department.
  To add a new department you click on the "New department" button and a the following dialog is shown  
  <img width="707" height="506" alt="image" src="https://github.com/user-attachments/assets/1af5d36a-cb05-47db-939e-d9e022263145" />  
  Fill out all the forms and click on "Save" and the department will be created..
  The little checkbox is there for marking the department as a surgery department that will containt operating rooms.
  To create a room, you first select a department from the left and click on the "New room" button that will present you the following dialog
  <img width="827" height="518" alt="image" src="https://github.com/user-attachments/assets/8a73d922-cda4-4d25-96e4-831efb098657" />  
  As shown in the picture if you haven't selected a department, you have to choose one from the combobox, and if you selected it previously it would be automatically selected in the combobox.
  Filling in the data and clicking on "Save" will create the room.


  
## 📌 Customization and Language

- You can change the application language by selecting the little gear icon in the top right corner
  <img width="498" height="291" alt="image" src="https://github.com/user-attachments/assets/7e53a26a-4275-4035-b793-9c9884457386" />  
  
- You can switch between Dark and Light themes by selecting the little gear icon in the top right corner
  <img width="447" height="287" alt="image" src="https://github.com/user-attachments/assets/15560061-738a-4595-957f-85592d413e2b" />  

- You can set the primary color by selecting the little gear icon in the top right corner
  <img width="437" height="316" alt="image" src="https://github.com/user-attachments/assets/2639a6d5-5db4-42b9-b646-6f422242ace9" />  

  Data about the language, theme and primary color are kept in a settings file, so every time you restart the application, it is in the previouslly selected language, theme and primary color
  




  



  











  







  


  
  


