using Application.DataTransfer.ProductDto;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models {
    public class CatalogViewModel {

        public PagedResponse<SearchProductsDto> Response { get; set; }
        public IEnumerable<SearchProductsDto> Products { get; set; }

    }
}
