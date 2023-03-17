using BookApp.Models.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("basket") ?? new SessionBasket();
            basket.Session = session;
            return basket;
        }
        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book Bk, int qty)
        {
            base.AddItem(Bk, qty);
            Session.SetJson("basket", this);
            
        }

        public override void RemoveItem(Book bk)
        {
            base.RemoveItem(bk);
            Session.SetJson("basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("basket");
        }

    }
}
