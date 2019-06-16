using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.ProductCommands;
using Application.DataTransfer.ProductDto;
using Application.Exceptions;
using DataAccess;

namespace EfCommands.EfProductCommands {
    public class EfCreateCommentCommand : BaseEfCommand, ICreateCommentCommand {
        public EfCreateCommentCommand(BigEmptyContext context) : base(context) {
        }

        public void Execute(CreateCommentDto request) {
            
            if(!Context.Products.Any(q => q.Id == request.ProductId)) {
                throw new EntityNotFoundException("Product");
            }

            Context.Comments.Add(new Domain.Comment {
                Text = request.Text,
                UserId = request.UserId,
                ProductId = request.ProductId
            });

            Context.SaveChanges();

        }
    }
}
