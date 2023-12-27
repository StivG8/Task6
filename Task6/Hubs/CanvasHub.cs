using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Task6.Models;
using Task6.Repositories;

namespace Task6.Hubs
{
    public class CanvasHub : Hub
    {
        private readonly ElementRepository elementRepository;

        public CanvasHub(ElementRepository elementRepository)
        {
            this.elementRepository = elementRepository;
        }

        public async Task AddDraw(Element newElement) => await Clients
            .All.SendAsync("ReceiveDraw", await elementRepository.Create(newElement));

        public async Task GetElemenets() => await Clients
            .Caller.SendAsync("InitialDraw", await this.elementRepository.Get());

        public async Task UpdateElement(Element updatedElement) => await Clients
            .AllExcept(Context.ConnectionId).SendAsync("ReceiveUpdatedElement", await this.elementRepository.Update(updatedElement));

        public async Task RemoveElement(Element newElement) => await Clients
            .All.SendAsync("ReceiveRemovedElement", await this.elementRepository.Remove(newElement));

    }
}
