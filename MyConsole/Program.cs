

using Consul;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Redlock.CSharp;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyConsole
{
    public class Program
    {

        public delegate void PrintStringMethodInvoker(string x);

        public static event PrintStringMethodInvoker PrintStringEvent;


        static void  Main(string[] args)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379")) {
                IDatabase db= redis.GetDatabase();
                
                while (true) {
                    var flag = db.LockTake("redislock", Thread.CurrentThread.ManagedThreadId, TimeSpan.FromSeconds(60));
                    if (flag) {
                        Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId} 加锁成功!");
                        break;
                    }
                    Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId} 加锁失败!");
                    Thread.Sleep(200);
                }
                
                db.StringIncrement("test", 1);
                var value = db.StringGet("test");
                Console.WriteLine($"test的值是{value}");

                while (true)
                {
                    var flag = db.LockRelease("redislock", Thread.CurrentThread.ManagedThreadId);
                    if (flag)
                    {
                        Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId} 解锁锁成功!");
                        break;
                    }
                    Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId} 解锁失败!");
                    Thread.Sleep(200);
                }
            }


            //using (RedisClient _client = new RedisClient("127.0.0.1", 6379)) {
            //    //_client.Set<int>("GoodNum", 100);




            //    //Parallel.For(1, 1000, (i) =>
            //    //{
            //    //    Task.Run(() =>
            //    //    {
            //    //        var dlm = new Redlock.CSharp.Redlock(
            //    //             ConnectionMultiplexer.Connect("127.0.0.1:6379")
            //    //         );

            //    //        // Declare lock object.
            //    //        Redlock.CSharp.Lock lockObject;

            //    //        // Try to acquire the lock (with resourceName as lock identifier and an 
            //    //        // expiration time of 10 seconds).
            //    //        var locked = dlm.Lock(
            //    //                "GoodNum",
            //    //                new TimeSpan(0, 0, 10),
            //    //                out lockObject
            //    //                 );

            //    //        // If locked is true, lockObject is populated and the lock has been acquired, 
            //    //        // otherwise the lock has not been acquired.

            //    //        // Tries to release the lock contained in lockObject.
            //    //        dlm.Unlock(lockObject);
            //    //    });



            //    //});
            //}

            Console.ReadLine();

        }

        public static void QiangGou() {
            Parallel.For(1, 1000000, (i) =>
            {
                getGoodResult(i);
            });
        }

        public static void getGoodResult(int i) {
            using (RedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                var ret = client.Get<int>("Onboarding_GoodNum_11");
                if (ret < 1) {
                    Console.WriteLine($"I:{i.ToString()}未抢到商品");
                    return;
                }

                var r1= client.DecrementValueBy("Onboarding_GoodNum_11", 1);
                if (r1 >= 0) {
                    client.EnqueueItemOnList("goodlist", i.ToString());
                }
              
            }
        }

        public static void SetGoodNum(int num) {
            using (RedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                client.Set<int>($"Onboarding_GoodNum_11", num);
            }
        }


        public static void SendEmail(string email, string Id) {
            using (RedisClient client = new RedisClient("127.0.0.1", 6379)) {
                var getValue= client.Get<string>($"Onboarding_SendEmail_{email}_{Id}");
                if (string.IsNullOrEmpty(getValue))
                {
                    client.Set<string>($"Onboarding_SendEmail_{email}_{Id}", $"发送给{email}的邮件");
                }
                else {
                    Console.WriteLine($"无需再次发送给{email}的邮件");
                    return;
                }
                Console.WriteLine($"发送给{email}的邮件");
            }
        }

        public static async Task<string> getDataAsnyc(int x) {
            await Task.Delay(1);
            return x.ToString();
        }


        public class Personel { 
          public string Id { get; set; }
          public string Name { get; set; }
          public string IdNumber { get; set; }
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

        public class Courses
        {
            public int Id { get; set; }

            public int StudentId { get; set; }

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
