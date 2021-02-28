using System;
using Forum.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAllPosts().Wait();
            GetAPost().Wait();
           // GetAPost(2).Wait(); //this is not working as it gives me an exception why?

            GetLastPosts().Wait();
            //GetLastPosts(2).Wait(); //same this is not working

            AddPost().Wait();
            GetAllPosts().Wait();

            Console.WriteLine();
            UpdatePost().Wait();

            Console.WriteLine();
            GetAllPosts().Wait();

            Console.WriteLine();
            DeletePost().Wait();
            GetAllPosts().Wait();
        }

        //Async Methods which call the restFul APIs

        //First API gets all posts 
        private static async Task GetAllPosts()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress =  new Uri("http://localhost:62111/");

                //3. Adding an accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.GetAsync("api/forum");

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt

                List<Post> posts = await response.Content.ReadAsAsync<List<Post>>();
                foreach (Post p in posts)
                {
                    Console.WriteLine("\n" + p.ToString());
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }



        //Second API gets a specified post
       
        //private static async Task GetAPost(int id)
        private static async Task GetAPost()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:62111/");

                //3. Adding an accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.GetAsync("api/forum/2");

              //HttpResponseMessage response = await client.GetAsync("api/forum/id");


                response.EnsureSuccessStatusCode(); // throws an exception if it isnt

                Post record = await response.Content.ReadAsAsync<Post>();

                Console.WriteLine("\n" + record.ToString());
                

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }


        //Third API gets last specified post

        //private static async Task GetLastPosts(int id)
        private static async Task GetLastPosts()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:62111/");

                //3. Adding an accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.GetAsync("api/forum/GetAnother/2");

               //  HttpResponseMessage response = await client.GetAsync("api/forum/GetAnother/id");


                response.EnsureSuccessStatusCode(); // throws an exception if it isnt

                List<Post> record = await response.Content.ReadAsAsync<List<Post>>();

                foreach(Post p in record)
                {
                    Console.WriteLine("\n" + p.ToString());
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }

        //Forth API creates a new post
        private static async Task AddPost()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:62111/");

                //3. Adding an accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. Creating a new entry from the body 
                Post newPost = new Post() { ID = 2, Subject = "Client Entry", Message = "This is a new entry from the client", TimeStamp = DateTime.Now };

                //5. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.PostAsJsonAsync("api/forum", newPost);

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Post was added!");


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }


        //Fifth API updates subject of an existing post
        private static async Task UpdatePost()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:62111/");

                //3. Updating the subject of a post 
                Post newSubject = new Post() { ID = 4, Subject = "Client Entry", Message = "This is a new entry from the client", TimeStamp = DateTime.Now };
                newSubject.Subject = "Entry Update";
                    

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.PutAsJsonAsync("api/forum/4", newSubject);

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Post Subject was updated!");


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }


        //Sixth API updates subject of an existing post
        private static async Task DeletePost()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:62111/");

                //3. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.DeleteAsync("api/forum/4");

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Post is now deleted!");


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }






    }
}
