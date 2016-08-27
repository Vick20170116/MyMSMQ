using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using MyEntity;//note:傳送接收端需引用相同物件

namespace MyMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                Console.Write("message sent,#{0}, time:{1} \r\n", i+1, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                SendMessage();
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


        static void SendMessage()
        {
            MessageQueue myQueue;
            //string path=@"TCP:192.168.0.104\Private$\MyQueue";
            string path = @".\Private$\MyQueue";
            if (MessageQueue.Exists(path))
            {
                myQueue = new MessageQueue(path);
            }
            else
            {
                myQueue = MessageQueue.Create(path);
            }

            List<Customer> customerList = new List<Customer>();
            customerList.Add(new Customer { CustomerID = "1", CustomerName = "Mike", Birthday = DateTime.Parse("1973/07/27"), Email = "yingchih.fang@gmail.com", CreateTime = DateTime.Now });
            customerList.Add(new Customer { CustomerID = "2", CustomerName = "Dan", Birthday = DateTime.Parse("1973/08/19"), Email = "danise0819@gmai.com", CreateTime = DateTime.Now });

            Message MyMessage = new Message();
            //set formatter for Message
            MyMessage.Formatter = new BinaryMessageFormatter();
            MyMessage.Body = customerList;
            MyMessage.Label = "customerList," + DateTime.Now.ToString("yyyyMMddHHmmss");
            MyMessage.Priority = MessagePriority.High;
            myQueue.Send(MyMessage);
        }

    }
}
