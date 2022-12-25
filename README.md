# MultiThread

iTextSharp, MultiThread example


This project demonstrates the use of multi-threading in C# to create multiple PDF files concurrently. The "LoadList" method generates a list of fake objects with various details, such as an ID, name, surname, birth date, and status. The "CreatePdf" method then creates a new thread for each object and creates a PDF file for that object, writing its details to the file. The threads are stored in a list, and when the list reaches a certain size (in this case, 5), the first thread in the list is made to wait (using the "Join" method) before being removed from the list. This ensures that the number of active threads at any given time is kept at a manageable level. The "CheckPath" method simply creates the specified directory if it does not already exist.
