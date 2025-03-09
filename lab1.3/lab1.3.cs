using System;


namespace lab1._3
{
    
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] pytanna = new string[] {
            "1. Професор ліг спати о 8 годині, а встав о 9 годині. Скільки годин проспав професор? (1) ",
            "2. На двох руках десять пальців. Скільки пальців на 10? (50) ",
            "3. Скільки цифр у дюжині? (2)",
            "4. Скільки потрібно зробити розпилів, щоб розпиляти колоду на 12 частин? (11) ", 
            "5. Лікар зробив три уколи в інтервалі 30 хвилин. Скільки часу він витратив? (30)", 
            "6. Скільки цифр 9 в інтервалі 1100? (1) " ,
            "7. Пастух мав 30 овець. Усі, окрім однієї, розбіглися. Скільки овець лишилося? (1) "
            };

            string[] vidpovidi = new string[] {
            "1","50","2","11","30","1","1",
            };

            int kilkistprav = 0;

            for (int i = 0; i < pytanna.Length; i++) 
            { 
                Console.WriteLine(pytanna[i]);
                string vidpovidkorys=Console.ReadLine();
                if(vidpovidkorys == vidpovidi[i])
                {
                    kilkistprav++;
                }
            }
            

            
            string[] ocinka = new string[] {
                "Геній","Ерудит","Нормальний","Здібності середні","Здібності нижче середнього","Вам треба відпочити!"
                };

            
            switch (kilkistprav) {
                case 7:
                        Console.WriteLine(ocinka[0]);
                    break;
                case 6:
                    
                        Console.WriteLine(ocinka[1]);
                     break;
                case 5:
                   
                        Console.WriteLine(ocinka[2]);
                    
                    break;
                case 4:
                    
                        Console.WriteLine(ocinka[3]);
                    break;
                case 3:
                    
                        Console.WriteLine(ocinka[4]);
                    
                    break;
                case <=2:
                    
                        Console.WriteLine(ocinka[5]);
                    
                    break;
            }
           
            
        }
    }
}
