﻿using DataAccess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace TaskManagement.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiBaseUrl"])
            };
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }
        public async Task<List<AssignedTask>> GetTasksAsync()
        {
            var response = await _httpClient.GetAsync("api/task");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AssignedTask>>(responseString, GetSerializerSettings());
        }

        public async Task<AssignedTask> GetTaskByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/task/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AssignedTask>(responseString, GetSerializerSettings());
        }

        public async Task AddTaskAsync(AssignedTask task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task, GetSerializerSettings()), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/task", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTaskAsync(AssignedTask task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task, GetSerializerSettings()), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/task/{task.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/task/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AssignedTask>> SearchTasksAsync(TaskSearchCriteria criteria)
        {
            var content = new StringContent(JsonConvert.SerializeObject(criteria, GetSerializerSettings()), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Task/search", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AssignedTask>>(responseString, GetSerializerSettings());
        }

        public async Task AddActivityAsync(Activity activity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(activity, GetSerializerSettings()), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Task/{activity.TaskId}/AddActivity", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(activity, GetSerializerSettings()), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/UpdateActivity/{activity.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Task/{id}/Activity");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Activity>(responseString, GetSerializerSettings());
        }

        public async Task DeleteActivityAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Task/Activity/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}