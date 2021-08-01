

using Consul;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyConsole
{
    public class Program
    {

        public delegate void PrintStringMethodInvoker(string x);

        public static event PrintStringMethodInvoker PrintStringEvent;


        static  void Main(string[] args)
        {
            

            ConsulHelp consul = new ConsulHelp(null, null);
            AgentService agentservice = consul.getRandomService("MyCoreAPI");
            string url = $"http://{agentservice.Address}:{agentservice.Port}/api/User/getValue";
            var aa=ApiHelp.GetInvoke(url);

        }

        public static async Task<List<string>> getData(int n) {
            List<string> list = new List<string>();
            await Task.Delay(1000);
            var num = Enumerable.Range(n, n + 50000000);
            num.ToList().ForEach(a =>
            {
                list.Add(a.ToString());
            });
            return list;
        }

        public static List<string> getDataList()
        {
            List<string> list = new List<string>();
            var num = Enumerable.Range(1,100000000);
            num.ToList().ForEach(a =>
            {
                list.Add(a.ToString());
            });
            return list;
        }




        [XmlRoot("Test")]
        public class Book
        { 
            [XmlIgnore]
            public string auth { get; set; }
            [XmlElement]
            public string Name { get; set; }
            [XmlAttribute]
            public string No { get; set; }

            [XmlIgnore]
            public string price { get; set; }
        }


      
       

        public static List<TaskTest1> GetTask1DataByMore()
        {
            var num = Enumerable.Range(1, 1000000).ToList();
            List<TaskTest1> taskList = new List<TaskTest1>();
            var a=Task.Run(() =>
            {
                num.ForEach(a => {
                    taskList.Add(new TaskTest1() { Id = Guid.NewGuid(), Name = "TaskData" + a });
                });
            });
            Task.WaitAll(a);
            return taskList;
        }

        public static List<TaskTest1> GetTask1Data() {
            var num = Enumerable.Range(1, 1000000).ToList();
            List<TaskTest1> taskList = new List<TaskTest1>();
            num.ForEach(a => {
                var n = a;
                taskList.Add(new TaskTest1() { Id = Guid.NewGuid(), Name = "TaskData" + n });
            });
            return taskList;
        }

        public static List<TaskTest2> GetTask2Data()
        {
            var num = Enumerable.Range(1, 1000000).ToList();
            List<TaskTest2> taskList = new List<TaskTest2>();
            num.ForEach(a => {
                var n = a;
                taskList.Add(new TaskTest2() { Id = Guid.NewGuid(), Name = "TaskData" + n });
            });
            return taskList;
        }


        public class Students { 
           public int Id { get; set; }

            public string Name { get; set; }
        }

        public static  void InsertTaskData() {
            Console.WriteLine("InsertTaskData开始" + DateTime.Now);
            var num = Enumerable.Range(1, 1000000).ToList();
            List<TaskTest1> taskList = new List<TaskTest1>();
            using (CoreDbContext _Context = new CoreDbContext()) {
                num.ForEach(a => {
                    var n = a;
                    taskList.Add(new TaskTest1() { Id = Guid.NewGuid(), Name = "TaskData" + n });
                });
                _Context.taskTest1s.AddRange(taskList);
                 _Context.SaveChanges();
            }
            Console.WriteLine("InsertTaskData结束" + DateTime.Now);
        }

        public static  void InsertTask2Data()
        {
            Console.WriteLine("InsertTask2Data开始" + DateTime.Now);
            var num = Enumerable.Range(1, 1000000).ToList();
            List<TaskTest2> taskList = new List<TaskTest2>();
            using (CoreDbContext _Context = new CoreDbContext())
            {
                num.ForEach(a => {
                    var n = a;
                    taskList.Add(new TaskTest2() { Id = Guid.NewGuid(), Name = "TaskData" + n });
                });
                _Context.taskTest2s.AddRange(taskList);
                 _Context.SaveChanges();
            }
            Console.WriteLine("InsertTask2Data结束" + DateTime.Now);
        }


        static void printMessage(string message)
        {
            Console.WriteLine("Message: {0}", message);
        }


        public static void TestTask1(Task task) {
            Console.WriteLine($"线程task1");
        }

        public static void TestTask2(Task task)
        {
            Console.WriteLine($"线程task2");
        }

        public static void TestTask3(Task task)
        {
            Console.WriteLine($"线程task3");
        }


        public static void Test1()
        {
            Console.WriteLine("执行Test");
            Console.WriteLine($"当前线程是{Thread.CurrentThread.ManagedThreadId}");
        }

        public static void TestString(object para1)
        {
            Console.WriteLine(para1.ToString() + "开始");
            Thread.CurrentThread.IsBackground = false;
            Thread.Sleep(10000);
            Console.WriteLine(para1.ToString() +"结束");
            //return para1.ToString();
        }

     

        public static void TestCall(IAsyncResult testCall) { 
        
        }


        public class TestCallBack : IAsyncResult
        {
            public object AsyncState => throw new NotImplementedException();

            public WaitHandle AsyncWaitHandle => throw new NotImplementedException();

            public bool CompletedSynchronously => true;

            public bool IsCompleted => true;
        }






        public static async Task PrintTaskNoReturn(string Msg) {
           await  Task.Delay(1000);
            Console.WriteLine(Msg);
        }

        public static async Task<string> PrintTaskHasReturn(string Msg)
        {
            await Task.Delay(1000);
            return Msg + "异步返回数据";
        }



    }
}
