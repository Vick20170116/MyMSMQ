using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using MyEntity;//note:傳送接收端需引用相同物件

namespace MyMSMQReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageQueue myMessageQueue;
            //string path=@"TCP:192.168.0.104\Private$\MyQueue";
            string path = @".\Private$\MyQueue";
            myMessageQueue = new MessageQueue(path);

            myMessageQueue.Formatter = new BinaryMessageFormatter();

            Message[] messages = myMessageQueue.GetAllMessages();
            int mesageCount = 0;
            Console.WriteLine("Receive MessageQueue from {0}", path);
            Console.WriteLine("messages size:{0}", messages.Length);
            foreach (Message message in messages)
            {
                mesageCount++;
                Message MyMessage = myMessageQueue.Receive();         
                List<Customer> customerList = (List<Customer>)MyMessage.Body;
                Console.WriteLine("Message #{0}", mesageCount);
                Console.WriteLine("customerList.count:{0}", customerList.Count);
                int count = 0;
                foreach (Customer customer in customerList)
                {
                    Console.WriteLine("Customer[{0}],CustomerID:{1},CustomerName:{2},CreateDate:{3} \r\n", count, customer.CustomerID, customer.CustomerName, customer.CreateTime.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    count++;
                }
                Console.WriteLine("");

            }

          
            Console.WriteLine("Press <ESC> to Quit!");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
                //Console.Write(cki.Key.ToString());
            } while (cki.Key != ConsoleKey.Escape);

        }

    }
}
