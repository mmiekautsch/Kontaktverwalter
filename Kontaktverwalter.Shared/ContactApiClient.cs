using Kontaktverwalter.Shared.DTO;
using System.Net.Http.Json;

namespace Kontaktverwalter.Shared
{
    public class ContactApiClient
    {
        private readonly HttpClient _httpClient;

        public ContactApiClient(string baseAddress = "https://localhost:1337") // should be read from configuration...
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<List<ContactDto>> GetAllContactsAsync()
        {
            var response = await _httpClient.GetAsync("api/contacts/list");
            return await ReadResponseAsync<List<ContactDto>>(response);
        }

        public async Task<List<ContactDto>> SearchContactsAsync(string query)
        {
            var response = await _httpClient.GetAsync($"api/contacts/{query}");
            return await ReadResponseAsync<List<ContactDto>>(response);
        }

        public async Task<ContactDetailDto> GetContactDetailsAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/contacts/details/{id}");
            return await ReadResponseAsync<ContactDetailDto>(response);
        }

        public async Task UpdateContactAsync(UpdateContactDto contact)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/contacts/update/{contact.Id}", contact);
            response.EnsureSuccessStatusCode();
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
        }
    }
}