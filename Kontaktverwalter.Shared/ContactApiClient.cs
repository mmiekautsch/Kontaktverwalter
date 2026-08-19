using Kontaktverwalter.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktverwalter.Shared
{
    public class ContactApiClient : IDisposable
    {
        // Static singleton instance
        private static readonly Lazy<ContactApiClient> _instance =
            new(() => new ContactApiClient());

        public static ContactApiClient Instance => _instance.Value;

        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        private ContactApiClient(string baseAddress = "https://localhost:1337")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        ~ContactApiClient()
        {
            Dispose(false);
        }

        public async Task<List<ContactDto>> GetAllContactsAsync()
        {
            using var response = await _httpClient.GetAsync("api/contacts/list");
            return await ReadResponseAsync<List<ContactDto>>(response);
        }

        public async Task<List<ContactDto>> SearchContactsAsync(string query)
        {
            using var response = await _httpClient.GetAsync($"api/contacts/{query}");
            return await ReadResponseAsync<List<ContactDto>>(response);
        }

        public async Task<ContactDetailsDto> GetContactDetailsAsync(long id)
        {
            using var response = await _httpClient.GetAsync($"api/contacts/details/{id}");
            return await ReadResponseAsync<ContactDetailsDto>(response);
        }

        public async Task UpdateContactAsync(UpdateContactDto contact)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/contacts/update/{contact.Id}", contact);
                using var content = response.Content;
                response.EnsureSuccessStatusCode();
                Trace.WriteLine("Contact updated successfully");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Failed to update contact", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new InvalidOperationException("Request content type is not supported", ex);
            }
        }

        private async Task<T> ReadResponseAsync<T>(HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>()
                    ?? throw new InvalidOperationException("Response content is null");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Failed to deserialize response content", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new InvalidOperationException("Response content type is not supported", ex);
            }
            finally
            {
                response.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _httpClient?.Dispose();
            }

            _disposed = true;
        }
    }
}