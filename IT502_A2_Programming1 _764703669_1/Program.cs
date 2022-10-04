/*Prgram Name: LANGHAM HOTELS MANAGEMENT SYSTEAM
 * Developer: Zih Hao Huang
 * Date:2/10/2022
 * Algorithm
 * 1. create list
 * 2. Implements its function according to the list, except for billing
 * 3. Increase the backup function and implement it
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace IT502_A2_Programming1__764703669_1
{
    public class Room
    {
        public int Roomnum { get; set; }
    }
    public class RoomAllocation
    {
        public int AllocationRoomNum { get; set; }
        public Guest AllocatedRoom { get; set; }
    }
    public class Guest
    {
        public string GuestName { get; set; }
    }
    internal class Program
    {
        public static Room[] listofrooms;
        public static Guest[] listofgust;

        public static List<RoomAllocation> listOfRoomAllocaltions = new List<RoomAllocation>();
        static void AddRooms()
        {
            try
            {
                Console.WriteLine("Please Enter How many Room you need:");
                int numRoom = Convert.ToInt32(Console.ReadLine());
                listofrooms = new Room[numRoom];
                for (int i = 0; i < numRoom; i++)
                {
                    Room room = new Room();
                    Console.WriteLine("Please Enter Room number :");
                    room.Roomnum = Convert.ToInt32(Console.ReadLine());

                    listofrooms[i] = room;
                }
            }
            catch { Console.WriteLine("Format Exception Please try again"); }
        }
        static void DisplayRooms()
        {
            try
            {
                foreach (Room room in listofrooms)
                {
                    Console.WriteLine("***********************************************\n");
                    Console.WriteLine(room.Roomnum + "\n");

                    Console.WriteLine("***********************************************\n");
                }
            }
            catch
            {
                Console.WriteLine("No information, please try again");
            }
        }

        static void AllocateRooms()
        {
            Console.WriteLine("Allocate Room for Guest");
            {
                try
                {
                    Console.WriteLine("Please Enter How many Guest:");
                    int numGuest = Convert.ToInt32(Console.ReadLine());
                    listofgust = new Guest[numGuest];
                    for (int n = 0; n < numGuest; n++)
                    {
                        Guest guest = new Guest();
                        Console.WriteLine("Please Enter  Guest Name :");
                        guest.GuestName = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Please Enter the Room Number:");
                        int RoomNum = Convert.ToInt32(Console.ReadLine());
                        listofgust[n] = guest;

                        bool roomFound = false;

                        for (int i = 0; i < listofrooms.Length; i++)
                        {
                            if (RoomNum == listofrooms[i].Roomnum)
                            {
                                roomFound = true;
                                break;
                            }
                        }
                        if (roomFound)
                        {
                            RoomAllocation roomAllocation = new RoomAllocation();
                            roomAllocation.AllocationRoomNum = RoomNum;
                            roomAllocation.AllocatedRoom = guest;

                            listOfRoomAllocaltions.Add(roomAllocation);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Room Number....");
                        }


                    }
                }
                catch { Console.WriteLine("Format Exception Please try again"); }

            }
        }
        static void DeAllocateRooms()
        {
            try
            {
                Console.WriteLine("DeAllocate Room function is called from Main function switch");
                Console.WriteLine("Please Enter the Room Number");
                int RoomNum = Convert.ToInt32(Console.ReadLine());

                bool roomFound = false;
                for (int i = 0; i < listofrooms.Length; i++)
                {
                    if (RoomNum == listofrooms[i].Roomnum)
                    {
                        roomFound = true;
                        break;
                    }
                }
                if (roomFound)
                {
                    RoomAllocation roomAllocation = listOfRoomAllocaltions.Find(x => x.AllocationRoomNum == RoomNum);
                    listOfRoomAllocaltions.Remove(roomAllocation);
                }
                else
                {
                    Console.WriteLine("Wrong Room Number...");
                }
            }
            catch { Console.WriteLine("No information, please try again"); }
        }
        static void DisplayRoomAllocationDetails()
        {
            Console.WriteLine("**************************** Rooms Allocations Details ****************************************");
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("Room Number\t Guest Name");
            Console.WriteLine("------------------------------------------------------------------------------------");

            foreach (RoomAllocation roomAllocation in listOfRoomAllocaltions)
            {
                Console.WriteLine(roomAllocation.AllocationRoomNum + "\t\t" + roomAllocation.AllocatedRoom.GuestName);
            }
        }
        static void Billing()
        {
            Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
        }
        public static string filePath;
        public static string BackupfilePath;
        static bool SavetheRoomDetailsToaFile()
        {//Use Bool to determine whether there is data input listOfRoomAllocaltions
            if (listOfRoomAllocaltions.Count <= 0)
            {
                return false;
            }

            using (FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {

                    Console.WriteLine("Your file has been saved");

                    StreamWriter streamWriter = new StreamWriter(f);
                    DateTime now = DateTime.Now;

                    foreach (RoomAllocation roomAllocation in listOfRoomAllocaltions)
                    {


                        string strToAdd = "Room Number: \t" + roomAllocation.AllocationRoomNum + ", Guest Name: \t" + roomAllocation.AllocatedRoom.GuestName + "\t" + now;
                        streamWriter.WriteLine(strToAdd);

                        streamWriter.Close();
                    }
                }
                catch
                {
                    return true;
                }
            }

            return true;

        }
        static void ShowtheRoomDetailsFromaFile()
        {
            try
            {
                FileStream f = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(f);



                string line = streamReader.ReadLine();

                while (line != null)

                {
                    Console.WriteLine(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch { Console.WriteLine("The file is missing, please try again"); }
        }
        static void BackupTheFile()
        {//If there is a backup with the same name in the folder,
         //it will fail to create,
         //so I added an if to detect if there is an old backup and delete it and create it
            if (File.Exists(BackupfilePath))
            {
                try
                {
                    Console.WriteLine("Found that old backup files have been deleted And backup the new file");
                    File.Delete(BackupfilePath);
                    File.Copy(filePath, BackupfilePath);
                    File.Delete(filePath);
                }
                catch
                {
                    File.Copy(filePath, BackupfilePath);

                }
            }
            else
            {
                File.Copy(filePath, BackupfilePath);
                Console.WriteLine("The file is Backup");
            }
        }
        static void Main(string[] args)
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderpath, "Lhms_764703669.txt");
            string folderpatha = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            BackupfilePath = Path.Combine(folderpatha, "Lhms_764703669_Backup.txt");
            char ans; int choice;
            try//If the user enters non-Y or y, an error will be displayed, please enter again
            {
                do
                {
                    try//This try is for the do loop, if the user enters a non-number, an error will be displayed, please enter a number
                    {
                        Console.Clear();
                        Console.WriteLine("***********************************************************************************");
                        Console.WriteLine("                 LANGHAM HOTELS MANAGEMENT SYSTEAM                  ");
                        Console.WriteLine("                               MENU                                 ");
                        Console.WriteLine("***********************************************************************************");
                        Console.WriteLine("1. Add Rooms");
                        Console.WriteLine("2. Display Rooms");
                        Console.WriteLine("3. Allocate Rooms");
                        Console.WriteLine("4. De-Allocate Rooms");
                        Console.WriteLine("5. Display Room Allocation Details");
                        Console.WriteLine("6. Billing");
                        Console.WriteLine("7. Save the Room Details To a File");
                        Console.WriteLine("8. Show the Room Details From a File");
                        Console.WriteLine("9. Exit");
                        Console.WriteLine("0. Backup The File");
                        Console.WriteLine("***********************************************************************************");
                        Console.Write("Enter Your Choice Number Here:");
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                AddRooms();


                                break;
                            case 2:
                                Console.Clear();
                                DisplayRooms();


                                break;
                            case 3:
                                Console.Clear();
                                AllocateRooms();


                                break;
                            case 4:
                                Console.Clear();
                                DeAllocateRooms();


                                break;
                            case 5:
                                Console.Clear();
                                DisplayRoomAllocationDetails();


                                break;
                            case 6:
                                Console.Clear();
                                Billing();


                                break;
                            case 7:
                                Console.Clear();
                                if (!SavetheRoomDetailsToaFile()) { Console.WriteLine("No information, please try again"); };


                                break;
                            case 8:
                                Console.Clear();
                                ShowtheRoomDetailsFromaFile();


                                break;
                            case 9:
                                Environment.Exit(0);

                                break;
                            case 0:
                                Console.Clear();
                                BackupTheFile();
                                break;

                            default:
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter Choice Number");

                    }
                    //Ask every time will back to SWITCH
                    Console.Write("\nWould You Like To Continue(Y/N):");
                    ans = Convert.ToChar(Console.ReadLine());
                } while (ans == 'y' || ans == 'Y');
            }
            catch
            {
                Console.WriteLine("Format Exception Please try again ");
            }
            finally
            {//will ask the user twice
                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
                if (ans == 'y' || ans == 'Y')
                {
                    Main(args);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
