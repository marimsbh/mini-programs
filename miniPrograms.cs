/* Your compile and run guide should be in the other panel, 
tabbed with the terminal window, which you can switch between

To build in the terminal, you need to type: mcs Source/Coursework.cs
And to run in the terminal, you need to type: mono Source/Coursework.exe
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


class MenuSystem 
{
    //representing the menu choices
    //https://www.w3schools.com/cs/cs_enums.php learned how to use enum 
    public enum menuChoices 
    {
        trinaryConverter = 'a',
        schoolRoster = 'b',
        ISBNVerifier = 'c',
        endProgram = 'q',
    }

    //method to display the menu choices
    public static void displayMenuChoices()
    {
        Console.Clear();
        Console.WriteLine("Choose one of the following options:\n-----------------------");
        Console.WriteLine("a - Trinary Converter.");
        Console.WriteLine("b - School Roster.");
        Console.WriteLine("c - ISBN Verifier.");
        Console.WriteLine("q - End program.");
        Console.WriteLine("Please enter your choice here:");

    }

    public static void Main(string[] args)
    {
        char menuInput;
        do
        {
            //calls method to display menu options
            displayMenuChoices();

            //gets and processes users menu input
            menuInput = Console.ReadKey().KeyChar;

            //runs mini program depending on users choice
            switch (menuInput)
            {
                case (char)menuChoices.trinaryConverter:
                    Console.WriteLine("\nRunning the Trinary Converter...");
                    trinaryConverter.Run();
                    break;
                
                case (char)menuChoices.schoolRoster:
                    Console.WriteLine("\nRunning the School Roster...");
                    schoolRoster.Run();
                    break;

                case (char)menuChoices.ISBNVerifier:
                    Console.WriteLine("\nRunning the ISBN Verifier...");
                    ISBNVerifier.Run();
                    break;
                
                case (char)menuChoices.endProgram:
                    Console.WriteLine("\nSee you next time! Exiting...");
                    break;

                default:
                    Console.WriteLine("\nError. Enter one of the options 'a', 'b', 'c' or 'q'.");
                    break;


            }
        //continues the loop until the user chooses 'q'
        } while (menuInput != (char)menuChoices.endProgram);
    }
}


//Trinary Converter mini program
class trinaryConverter
{
    //defines an array to hold allowed trinary validTrinaryInputs
    //note: using an arry makes it more easily modified
    private static int[] validTrinaryInputs = {0, 1, 2};

    //method to run main program
    public static void Run()
    {
        do
        {

            Console.WriteLine("Please enter your trinary number here:");
            string trinaryNum = Console.ReadLine();

            //checks whether the trinary number entered is valid
            if (IsTrinaryNumValid(trinaryNum))
            {
                //if valid, converts the trinary num to decimal equivalent 
                int decimalResult = decimalConverter(trinaryNum);
                Console.WriteLine($"The decimal equivalent of {trinaryNum} is {decimalResult}.");

                //prompts the user to either try again or return to menu
                Console.WriteLine("Enter 'Y' to try again or any other key to return to main menu.");
                string returnToMenu = Console.ReadLine().ToLower();

                //breaks out of the do loop if 'y' isn't entered
                if (returnToMenu != "y")
                {
                    Console.WriteLine("Returning to main menu...");
                    break;
                }
            }
            else
            {
                //error handling if trinary number isn't correct format
                Console.WriteLine("Error. Enter only the valid trinary numbers '0s, '1s', '2s'.");
            }

        } while (true);
    }

    //method to check if trinary number is valid
    //https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
    //note: instead of just checking its a digit from the example above, i changed it to check that each 
    //character is in the valid input array
    public static bool IsTrinaryNumValid(string trinaryNum)
    {
        foreach (char n in trinaryNum)
        {
            //subtarcting 0 to convert it to corresponding numeric value
            if (!validTrinaryInputs.Contains(n - '0'))
            {
                //if any character isn't valid, returns false
                return false;
            }
        }
        //runs if loop in trinary run method
        return true;

    }

    //method to calculate trinary to decimal
    public static int decimalConverter(string trinary)
    {
        int decimalNum = 0;

        //loops through each trinary digit
        for (int i = trinary.Length - 1, powerNum = 1; i >=0; i--, powerNum *= 3)
        {
            //takes the current trinary digit
            char digit = trinary[i];

            //converts it to its numeric value
            int verifiedTrinary = validTrinaryInputs[digit - '0'];

            decimalNum += verifiedTrinary * powerNum;
        }
        return decimalNum;
    }

}


//school roster mini program
class schoolRoster
{
    //constant to file that contains roster data
    //note: avoids hardcoding file path in each method
    private const string rosterFile = "Source/roster_data.txt";
    
    //representing roster's menu choices
    //https://www.w3schools.com/cs/cs_enums.php
    public enum rosterChoices
    {
        addStudentToForm = 'a',
        studentsByForm = 'b',
        sortedStudentsList = 'c',
        returnToMenu = 'q',
    }

    //method to display roster's menu choices
    public static void displayRosterChoices()
    {
        Console.WriteLine("Choose one of the following options:\n------------------------");
        Console.WriteLine("a - Add a student to a chosen form.");
        Console.WriteLine("b - Get a list of students enrolled in a form.");
        Console.WriteLine("c - Get a list of all students in the roster.");
        Console.WriteLine("q - Return to main menu.");
        Console.WriteLine("Please enter your choice here:");
    }

    //main method to run roster's menu system allowing user to pick choice
    public static void Run()
    {
        char rosterInput;

        do
        {
            //calls method to display roster's menu options
            displayRosterChoices();
            rosterInput = Console.ReadKey().KeyChar;

            //runs mini program depending on users choice
            switch(rosterInput)
            {
                case (char)rosterChoices.addStudentToForm:
                    addStudentToForm();
                    break;
                
                case (char)rosterChoices.studentsByForm:
                    studentsByForm();
                    break;

                case (char)rosterChoices.sortedStudentsList:
                    sortedStudentsList();
                    break;

                case (char)rosterChoices.returnToMenu:
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("\nError. Enter one of the options 'a', 'b', 'c' or 'q'.");
                    break;

            }
        } while (rosterInput != (char)rosterChoices.returnToMenu);

    }


    //method to add student to a chosen form
    public static void addStudentToForm()
    {
          Console.WriteLine("Enter the student you want to add:");
          string studentName = Console.ReadLine().ToLower();

          Console.WriteLine($"Enter the form to add {studentName} to:");
          string chosenForm = Console.ReadLine();

          //makes sure both name and form are valid format by calling methods to check
          if (IsValidStudentName(studentName) && IsValidChosenForm(chosenForm))
          {
                string studentData = $"{studentName}, {chosenForm}";
                //microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file
                //learnt how to use streamwriter to write to files
                using (StreamWriter writer = new StreamWriter(rosterFile, true))
                {
                    //writes the chosen data to the roster file
                    writer.WriteLine(studentData);
                    Console.WriteLine($"Completed! {studentName} has been added to form {chosenForm}.");
                }
          }
          else
          {
              Console.WriteLine("Error. Make sure name and form isn't empty and contains only letters.");
          }
    }

    //method to display students from singular chosen form
    public static void studentsByForm()
    {
        //https://www.w3schools.com/cs/cs_exceptions.php try and catch cs_exceptions
        //we did this in sessions but i used this to help too
        try
        {
            
            Console.WriteLine("Enter the form to display students from:");
            string chosenForm = Console.ReadLine();

            //creates file if doesnt exist and reads all lines in file
            createFile();
            string[] lines = File.ReadAllLines(rosterFile);
            
            //list to store students extarcted from file in user's chosen form
            List<string> studentsInForm = new List<string>();

            //iterates through the file and prints names from chosen form
            foreach (string line in lines)
            {
                var (nameSection, formSection) = extractNameAndForm(line);

                //https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinalignorecase?view=net-8.0
                //above link for StringComparison.OrdinalIgnoreCase learning to compare strings
                if (formSection.Equals(chosenForm, StringComparison.OrdinalIgnoreCase))
                {
                  studentsInForm.Add(nameSection);
                }
            }

            if (studentsInForm.Any())
            {
                //https://stackoverflow.com/questions/1330003/how-to-join-a-generic-list-of-objects-on-a-specific-property
                //learned how to join items from lists with ',' to make it look more clean when printed
                string completeStudents = string.Join(",", studentsInForm);
                Console.WriteLine($"The students in form {chosenForm} are {completeStudents}");
            }
            else
            {
              Console.WriteLine("Error. Form doesn't exist.");
            }

        }
        catch (IOException e)
        {
            Console.WriteLine("IOException " + e.ToString());
        }
    }

    //method to display all students in roster in order
    public static void sortedStudentsList()
    {
        try
        {
            //creates file if doesnt exist and reads all lines in file
            createFile();
            string[] lines = File.ReadAllLines(rosterFile);

            Array.Sort(lines, Compare);

            Console.WriteLine("The students currently in the roster are:");
            //https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split
            foreach (var element in lines)
            {
                var (nameSection, formSection)= extractNameAndForm(element);

                Console.WriteLine($"{nameSection}, {formSection}");

            }
        }
        catch (IOException e)
        {
            Console.WriteLine("IOException " + e.ToString());
        }
    }

    //method to get names and forms separtely from file
    public static (string name, string form) extractNameAndForm(string line)
    {
        //data is stored as 'student, form' so this seperates student and form by comma to compare 
        string [] dividedParts = line.Split(',');

        //makes sure the format of 'student, form' is correct
        if (dividedParts.Length == 2)
        {
            //https://www.geeksforgeeks.org/c-sharp-trim-method/ learning Trim()
            string nameSection = dividedParts[0].Trim();
            string formSection = dividedParts[1].Trim(); 
            return (nameSection, formSection);
        }
        else
        {
            //error handling to see where invalid format is if there's one
            Console.WriteLine($"Invalid format in line: {line}");
            return (string.Empty, string.Empty);
        }
        
    }


    //------------- quick sort methods -------------------
    //https://www.youtube.com/watch?v=wygsfgtpApI
    //https://stackoverflow.com/questions/13648252/implementing-quicksort-algorithm
    //helped me with the quicksort main format 

    public static int Compare(string line1, string line2)
    {
        //splits the string into two parts using the comma as the splitting section
        string [] part1 = line1.Split(',');
        string [] part2 = line2.Split(',');

        //checks if both parts have the expeceted format
        if (part1.Length == 2 && part2.Length ==2)
        {

            //extracts the form numbers to compare each other
            //obtained by parsing the second element of parts[1]
            int form1 = int.Parse(part1[1].Trim());
            int form2 = int.Parse(part2[1].Trim());
           
            //compares forms
            if (form1 < form2)
            {
              //if form1 is smaller than form2 returns interger less than 0
              //shows form1 should be before form2 in sorted order
              return -1;

            }

            else if (form1 > form2)
            {
              //if form1 is bigger than form2 returns interger bigger than 0
              //shows form1 should be after form2 in sorted order
              return 1;

            }

            else
            {

              //if the form numbers are equal, next compare the names
              string name1 = part1[0].Trim();
              string name2 = part2[0].Trim();

              return name1.CompareTo(name2);

            }

        }
        //error handling default case if the lines don't meet correct format
        return 0;
    }

    //method to exchange the elements in the array to sort them
    public static void Exchange (string[] lines, int i, int j)
    {
        string temp = lines[i];
        lines[i] = lines[j];
        lines[j] = temp;
    }

    //-------------------- error handling -------------------


    //creates the file if it doesn't exist
    public static void createFile()
    {
      if (!File.Exists(rosterFile))
      {
        File.Create(rosterFile).Close();
      }
    }

    //https://www.geeksforgeeks.org/lambda-expressions-in-c-sharp/
    //method to check the student name input is valid
    public static bool IsValidStudentName(string studentName)
    {
      return !string.IsNullOrEmpty(studentName) && studentName.All(char.IsLetter);
    }

    //method to check the chosen form is valid 
    public static bool IsValidChosenForm(string chosenForm)
    {
      return !string.IsNullOrEmpty(chosenForm) && chosenForm.All(c => char.IsDigit(c) || char.IsWhiteSpace(c)); 
    }

}

//ISBN Verifier mini program
class ISBNVerifier
{
  //main method to run ISBN verification process
  public static void Run()
  {
    //boolean variable, if it becomes false exits ISBN verifier
    bool continueISBN = true;

    do
    {
          Console.WriteLine("Enter your ISBN input here:");
          string ISBNInput = Console.ReadLine().ToLower();


          if (checkISBNFormat(ISBNInput))
          {
            Console.WriteLine($"Your ISBN {ISBNInput} is valid!"); 
            continueISBN = returnToMenuChoice();

          }
          else
          {
            Console.WriteLine($"Your ISBN {ISBNInput} isn't valid."); 
            continueISBN = returnToMenuChoice();

          }

    } while (continueISBN);

  }

  //method to give user option to try ISBNVerifier again or return 
  public static bool returnToMenuChoice()
  {
        Console.WriteLine("Press key 'Y' to try again or any other key to return to main menu");
        string returnToMenu = Console.ReadLine().ToLower();

        return returnToMenu == "y";
  }

  //checks ISBN input is valid
  public static bool checkISBNFormat(string ISBNInput)
  {
        //removes the hyphens
        string simplifiedISBN = ISBNInput.Replace("-", "");
        

        //checking if ISBN is correct length
        if (simplifiedISBN.Length != 10)
        {
          Console.WriteLine("Incorrect ISBN length.");
          return false;

        }


        //checking the checker character is correct
        if (simplifiedISBN[9] == 'x')
        {
          simplifiedISBN = simplifiedISBN.Replace("x", "10");
        }
        else if (!char.IsDigit(simplifiedISBN[9]))
        {
          Console.WriteLine("Checker digit needs to be 'X' or a digit.");
          return false;
        }


        //calls the method to check the validity 
        return IsISBNValid(simplifiedISBN);
        

  }

  //method to calculate if ISBN is = 0 (valid)
  public static bool IsISBNValid(string simplifiedISBN)
  {
    int finalSum = 0;
    //iteartes over the 10 characters of simplifiedISBN 
    for (int i = 0; i < 10; i ++)
    {
      //converts the caharcter to its numeric value and multiples it by (10-1)
      //with each iteration the multiplication number decreases by 1 until it reaches 0
      finalSum += (simplifiedISBN[i] - '0') * (10-i);
    }

    return finalSum % 11 == 0;

  }

}







