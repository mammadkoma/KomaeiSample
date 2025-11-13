namespace KomaeiSample.Server.Services;
public class OrderService(AppDbContext appDbContext, IHttpContextAccessor httpContext, FileService fileService, SmsService smsService)
{
    public async Task<OrderDto> GetAll(Guid? addUserId, bool isForCard)
    {
        var orderDto = new OrderDto();
        var queryOffice =
            from order in appDbContext.Orders.AsNoTracking()
            join envelopeOffice in appDbContext.EnvelopeOffices.AsNoTracking() on order.EnvelopeId equals envelopeOffice.Id
            join model in appDbContext.Models.AsNoTracking() on envelopeOffice.ModelId equals model.Id
            join paper in appDbContext.Papers.AsNoTracking() on envelopeOffice.PaperId equals paper.Id
            join grammage in appDbContext.Grammages.AsNoTracking() on envelopeOffice.GrammageId equals grammage.Id
            join count in appDbContext.Counts.AsNoTracking() on envelopeOffice.CountId equals count.Id
            join cellophane in appDbContext.Cellophanes.AsNoTracking() on order.CellophaneId equals cellophane.Id into cellophaneJoin
            from cellophane in cellophaneJoin.DefaultIfEmpty()
            join uv in appDbContext.Uvs.AsNoTracking() on order.UvId equals uv.Id into uvJoin
            from uv in uvJoin.DefaultIfEmpty()
            where order.CategoryId == CategoriesEnum.Office.ToInt()
            select new OrderOfficeDto
            {
                Id = order.Id,
                ModelTitle = model.Title,
                PaperTitle = paper.Title,
                GrammageTitle = grammage.Title,
                CountTitle = (count.Title * order.Series).ToString(),
                CellophaneTitle = cellophane.Title.ToString(),
                UvTitle = uv.Title.ToString(),
                HasInternalTeramTitle = envelopeOffice.HasInternalTeram == 1 ? "دارد" : "ندارد",
                HasDoorGlueTitle = envelopeOffice.HasDoorGlue == 1 ? "دارد" : "ندارد",
                Price = order.Price * order.Series,
                PriceAfterDiscount = order.PriceAfterDiscount,
                Series = order.Series,
                DeliveryMethodTitle = order.DeliveryMethod.Title,
                AddressTypeTitle = order.AddressType!.Title,
                TehranAreaTitle = order.TehranArea!.Title,
                Address = order.Address ?? "",
                PostalCode = order.PostalCode ?? "",
                AddDate = order.AddDate,
                OrderStatusId = order.OrderStatusId,
                OrderStatusTitle = order.OrderStatus.Title,
                AddUserFullName = order.AddUser.FullName,
                AddUserMobile = order.AddUser.Mobile,
                FileName = order.FileName,
                FileExtension = order.FileExtension,
                Desc = order.Desc ?? "",
                PayDate = order.PayDate ?? null,
                AddUserId = order.AddUserId,
            };

        var queryHospital =
            from order in appDbContext.Orders.AsNoTracking()
            join envelopeHospital in appDbContext.EnvelopeHospitals.AsNoTracking() on order.EnvelopeId equals envelopeHospital.Id
            join model in appDbContext.Models.AsNoTracking() on envelopeHospital.ModelId equals model.Id
            join hospitalTemplate in appDbContext.HospitalTemplates.AsNoTracking() on envelopeHospital.HospitalTemplateId equals hospitalTemplate.Id
            join paper in appDbContext.Papers.AsNoTracking() on envelopeHospital.PaperId equals paper.Id
            join grammage in appDbContext.Grammages.AsNoTracking() on envelopeHospital.GrammageId equals grammage.Id
            join count in appDbContext.Counts.AsNoTracking() on envelopeHospital.CountId equals count.Id
            join cellophane in appDbContext.Cellophanes.AsNoTracking() on order.CellophaneId equals cellophane.Id into cellophaneJoin
            from cellophane in cellophaneJoin.DefaultIfEmpty()
            join uv in appDbContext.Uvs.AsNoTracking() on order.UvId equals uv.Id into uvJoin
            from uv in uvJoin.DefaultIfEmpty()
            where order.CategoryId == CategoriesEnum.Hospital.ToInt()
            select new OrderHospitalDto
            {
                Id = order.Id,
                ModelTitle = model.Title,
                HospitalTemplateTemplateName = hospitalTemplate.TemplateName,
                PaperTitle = paper.Title,
                GrammageTitle = grammage.Title,
                CountTitle = (count.Title * order.Series).ToString(),
                CellophaneTitle = cellophane.Title,
                UvTitle = uv.Title,
                Price = order.Price * order.Series,
                PriceAfterDiscount = order.PriceAfterDiscount,
                DeliveryMethodTitle = order.DeliveryMethod.Title,
                AddressTypeTitle = order.AddressType!.Title,
                TehranAreaTitle = order.TehranArea!.Title,
                Address = order.Address ?? "",
                PostalCode = order.PostalCode ?? "",
                AddDate = order.AddDate,
                OrderStatusId = order.OrderStatusId,
                OrderStatusTitle = order.OrderStatus.Title,
                AddUserFullName = order.AddUser.FullName,
                AddUserMobile = order.AddUser.Mobile,
                FileName = order.FileName,
                FileExtension = order.FileExtension,
                Desc = order.Desc ?? "",
                PayDate = order.PayDate ?? null,
                AddUserId = order.AddUserId,
            };

        var queryHandBag =
            from order in appDbContext.Orders.AsNoTracking()
            join envelopeHandBag in appDbContext.EnvelopeHandBags.AsNoTracking() on order.EnvelopeId equals envelopeHandBag.Id
            join model in appDbContext.Models.AsNoTracking() on envelopeHandBag.ModelId equals model.Id
            join paper in appDbContext.Papers.AsNoTracking() on envelopeHandBag.PaperId equals paper.Id
            join grammage in appDbContext.Grammages.AsNoTracking() on envelopeHandBag.GrammageId equals grammage.Id
            join count in appDbContext.Counts.AsNoTracking() on envelopeHandBag.CountId equals count.Id
            join cellophane in appDbContext.Cellophanes.AsNoTracking() on order.CellophaneId equals cellophane.Id into cellophaneJoin
            from cellophane in cellophaneJoin.DefaultIfEmpty()
            join uv in appDbContext.Uvs.AsNoTracking() on order.UvId equals uv.Id into uvJoin
            from uv in uvJoin.DefaultIfEmpty()
            where order.CategoryId == CategoriesEnum.HandBag.ToInt()
            select new OrderHandBagDto
            {
                Id = order.Id,
                ModelTitle = model.Title,
                PaperTitle = paper.Title,
                GrammageTitle = grammage.Title,
                CountTitle = (count.Title * order.Series).ToString(),
                CellophaneTitle = cellophane.Title,
                UvTitle = uv.Title,
                Price = order.Price * order.Series,
                PriceAfterDiscount = order.PriceAfterDiscount,
                DeliveryMethodTitle = order.DeliveryMethod.Title,
                AddressTypeTitle = order.AddressType!.Title,
                TehranAreaTitle = order.TehranArea!.Title,
                Address = order.Address ?? "",
                PostalCode = order.PostalCode ?? "",
                AddDate = order.AddDate,
                OrderStatusId = order.OrderStatusId,
                OrderStatusTitle = order.OrderStatus.Title,
                AddUserFullName = order.AddUser.FullName,
                AddUserMobile = order.AddUser.Mobile,
                FileName = order.FileName,
                FileExtension = order.FileExtension,
                Desc = order.Desc ?? "",
                PayDate = order.PayDate ?? null,
                AddUserId = order.AddUserId,
            };

        var queryConfidential =
            from order in appDbContext.Orders.AsNoTracking()
            join envelopeConfidential in appDbContext.EnvelopeConfidentials.AsNoTracking() on order.EnvelopeId equals envelopeConfidential.Id
            join model in appDbContext.Models.AsNoTracking() on envelopeConfidential.ModelId equals model.Id
            join paper in appDbContext.Papers.AsNoTracking() on envelopeConfidential.PaperId equals paper.Id
            join grammage in appDbContext.Grammages.AsNoTracking() on envelopeConfidential.GrammageId equals grammage.Id
            join count in appDbContext.Counts.AsNoTracking() on envelopeConfidential.CountId equals count.Id
            where order.CategoryId == CategoriesEnum.Confidential.ToInt()
            select new OrderConfidentialDto
            {
                Id = order.Id,
                ModelTitle = model.Title,
                PaperTitle = paper.Title,
                GrammageTitle = grammage.Title,
                CountTitle = (count.Title * order.Series).ToString(),
                Price = order.Price * order.Series,
                PriceAfterDiscount = order.PriceAfterDiscount,
                DeliveryMethodTitle = order.DeliveryMethod.Title,
                AddressTypeTitle = order.AddressType!.Title,
                TehranAreaTitle = order.TehranArea!.Title,
                Address = order.Address ?? "",
                PostalCode = order.PostalCode ?? "",
                AddDate = order.AddDate,
                OrderStatusId = order.OrderStatusId,
                OrderStatusTitle = order.OrderStatus.Title,
                AddUserFullName = order.AddUser.FullName,
                AddUserMobile = order.AddUser.Mobile,
                FileName = order.FileName,
                FileExtension = order.FileExtension,
                Desc = order.Desc ?? "",
                PayDate = order.PayDate ?? null,
                AddUserId = order.AddUserId,
            };

        if (addUserId != null) queryOffice = queryOffice.Where(x => x.AddUserId == addUserId);
        if (isForCard == true) queryOffice = queryOffice.Where(x => x.OrderStatusId == OrderStatusesEnum.Card.ToInt());
        if (isForCard == false) queryOffice = queryOffice.Where(x => x.OrderStatusId != OrderStatusesEnum.Card.ToInt());

        if (addUserId != null) queryHospital = queryHospital.Where(x => x.AddUserId == addUserId);
        if (isForCard == true) queryHospital = queryHospital.Where(x => x.OrderStatusId == OrderStatusesEnum.Card.ToInt());
        if (isForCard == false) queryHospital = queryHospital.Where(x => x.OrderStatusId != OrderStatusesEnum.Card.ToInt());

        if (addUserId != null) queryHandBag = queryHandBag.Where(x => x.AddUserId == addUserId);
        if (isForCard == true) queryHandBag = queryHandBag.Where(x => x.OrderStatusId == OrderStatusesEnum.Card.ToInt());
        if (isForCard == false) queryHandBag = queryHandBag.Where(x => x.OrderStatusId != OrderStatusesEnum.Card.ToInt());

        if (addUserId != null) queryConfidential = queryConfidential.Where(x => x.AddUserId == addUserId);
        if (isForCard == true) queryConfidential = queryConfidential.Where(x => x.OrderStatusId == OrderStatusesEnum.Card.ToInt());
        if (isForCard == false) queryConfidential = queryConfidential.Where(x => x.OrderStatusId != OrderStatusesEnum.Card.ToInt());

        orderDto.OrderOfficeDtos = await queryOffice.OrderByDescending(x => x.Id).Take(200).ToArrayAsync();
        orderDto.OrderHospitalDtos = await queryHospital.OrderByDescending(x => x.Id).Take(200).ToArrayAsync();
        orderDto.OrderHandBagDtos = await queryHandBag.OrderByDescending(x => x.Id).Take(200).ToArrayAsync();
        orderDto.OrderConfidentialDtos = await queryConfidential.OrderByDescending(x => x.Id).Take(200).ToArrayAsync();

        if (addUserId != null)
            orderDto.WalletBalance = await appDbContext.Users.AsNoTracking().Where(x => x.Id == httpContext.GetUserId()).Select(x => x.WalletBalance).FirstAsync();

        return orderDto;
    }

    public async Task<InvoiceDto> GetInvoiceData(int Id)
    {
        var CategoryId = (await appDbContext.Orders.AsNoTracking().FirstAsync(x => x.Id == Id)).CategoryId;
        if (CategoryId == CategoriesEnum.Office.ToInt())
            return await (
                from order in appDbContext.Orders
                join envelopeOffice in appDbContext.EnvelopeOffices on order.EnvelopeId equals envelopeOffice.Id
                join category in appDbContext.Categories on order.CategoryId equals category.Id
                join model in appDbContext.Models on envelopeOffice.ModelId equals model.Id
                join paper in appDbContext.Papers on envelopeOffice.PaperId equals paper.Id
                join grammage in appDbContext.Grammages on envelopeOffice.GrammageId equals grammage.Id
                join count in appDbContext.Counts on envelopeOffice.CountId equals count.Id
                join cellophane in appDbContext.Cellophanes on order.CellophaneId equals cellophane.Id into cellophaneJoin
                from cellophane in cellophaneJoin.DefaultIfEmpty()
                join uv in appDbContext.Uvs on order.UvId equals uv.Id into uvJoin
                from uv in uvJoin.DefaultIfEmpty()
                where order.Id == Id
                select new InvoiceDto
                {
                    Id = order.Id,
                    CategoryTitle = category.Title,
                    ModelTitle = envelopeOffice.Model.Title,
                    PaperTitle = paper.Title,
                    GrammageTitle = grammage.Title,
                    CountTitle = (count.Title * order.Series).ToString(),
                    Price = order.Price * order.Series,
                    PriceAfterDiscount = order.PriceAfterDiscount,
                    AddDate = order.AddDate,
                    AddUserFullName = order.AddUser.FullName,
                    AddUserMobile = order.AddUser.Mobile,
                    PayDate = order.PayDate,
                }).FirstAsync();

        else if (CategoryId == CategoriesEnum.Hospital.ToInt())
            return await (
                from order in appDbContext.Orders
                join envelopeHospital in appDbContext.EnvelopeHospitals on order.EnvelopeId equals envelopeHospital.Id
                join category in appDbContext.Categories on order.CategoryId equals category.Id
                join model in appDbContext.Models on envelopeHospital.ModelId equals model.Id
                join hospitalTemplate in appDbContext.HospitalTemplates on envelopeHospital.HospitalTemplateId equals hospitalTemplate.Id
                join paper in appDbContext.Papers on envelopeHospital.PaperId equals paper.Id
                join grammage in appDbContext.Grammages on envelopeHospital.GrammageId equals grammage.Id
                join count in appDbContext.Counts on envelopeHospital.CountId equals count.Id
                join cellophane in appDbContext.Cellophanes on order.CellophaneId equals cellophane.Id into cellophaneJoin
                from cellophane in cellophaneJoin.DefaultIfEmpty()
                join uv in appDbContext.Uvs on order.UvId equals uv.Id into uvJoin
                from uv in uvJoin.DefaultIfEmpty()
                where order.Id == Id
                select new InvoiceDto
                {
                    Id = order.Id,
                    CategoryTitle = category.Title,
                    ModelTitle = envelopeHospital.Model.Title,
                    EnvelopeHospitalTemplateName = hospitalTemplate.TemplateName,
                    PaperTitle = paper.Title,
                    GrammageTitle = grammage.Title,
                    CountTitle = (count.Title * order.Series).ToString(),
                    Price = order.Price * order.Series,
                    PriceAfterDiscount = order.PriceAfterDiscount,
                    AddDate = order.AddDate,
                    AddUserFullName = order.AddUser.FullName,
                    AddUserMobile = order.AddUser.Mobile,
                    PayDate = order.PayDate,
                }).FirstAsync();

        else if (CategoryId == CategoriesEnum.HandBag.ToInt())
            return await (
                from order in appDbContext.Orders
                join envelopeHandBag in appDbContext.EnvelopeHandBags on order.EnvelopeId equals envelopeHandBag.Id
                join category in appDbContext.Categories on order.CategoryId equals category.Id
                join model in appDbContext.Models on envelopeHandBag.ModelId equals model.Id
                join paper in appDbContext.Papers on envelopeHandBag.PaperId equals paper.Id
                join grammage in appDbContext.Grammages on envelopeHandBag.GrammageId equals grammage.Id
                join count in appDbContext.Counts on envelopeHandBag.CountId equals count.Id
                join cellophane in appDbContext.Cellophanes on order.CellophaneId equals cellophane.Id into cellophaneJoin
                from cellophane in cellophaneJoin.DefaultIfEmpty()
                join uv in appDbContext.Uvs on order.UvId equals uv.Id into uvJoin
                from uv in uvJoin.DefaultIfEmpty()
                where order.Id == Id
                select new InvoiceDto
                {
                    Id = order.Id,
                    CategoryTitle = category.Title,
                    ModelTitle = envelopeHandBag.Model.Title,
                    PaperTitle = paper.Title,
                    GrammageTitle = grammage.Title,
                    CountTitle = (count.Title * order.Series).ToString(),
                    Price = order.Price * order.Series,
                    PriceAfterDiscount = order.PriceAfterDiscount,
                    AddDate = order.AddDate,
                    AddUserFullName = order.AddUser.FullName,
                    AddUserMobile = order.AddUser.Mobile,
                    PayDate = order.PayDate,
                }).FirstAsync();

        else if (CategoryId == CategoriesEnum.Confidential.ToInt())
            return await (
                from order in appDbContext.Orders
                join envelopeConfidential in appDbContext.EnvelopeConfidentials on order.EnvelopeId equals envelopeConfidential.Id
                join category in appDbContext.Categories on order.CategoryId equals category.Id
                join model in appDbContext.Models on envelopeConfidential.ModelId equals model.Id
                join paper in appDbContext.Papers on envelopeConfidential.PaperId equals paper.Id
                join grammage in appDbContext.Grammages on envelopeConfidential.GrammageId equals grammage.Id
                join count in appDbContext.Counts on envelopeConfidential.CountId equals count.Id
                where order.Id == Id
                select new InvoiceDto
                {
                    Id = order.Id,
                    CategoryTitle = category.Title,
                    ModelTitle = envelopeConfidential.Model.Title,
                    PaperTitle = paper.Title,
                    GrammageTitle = grammage.Title,
                    CountTitle = (count.Title * order.Series).ToString(),
                    Price = order.Price * order.Series,
                    PriceAfterDiscount = order.PriceAfterDiscount,
                    AddDate = order.AddDate,
                    AddUserFullName = order.AddUser.FullName,
                    AddUserMobile = order.AddUser.Mobile,
                    PayDate = order.PayDate,
                }).FirstAsync();

        else return new InvoiceDto();
    }

    public async Task<int> Add(OrderVm vm, IFormFile file)
    {
        if (vm.PostalCode != null)
        {
            if (long.TryParse(vm.PostalCode, out var postalCode) == false)
                throw new AppException("کد پستی معتبر نیست");
        }
        var newRecord = vm.Adapt<Order>();
        newRecord.FileName = Guid.NewGuid().ToString();
        newRecord.FileExtension = Path.GetExtension(file.FileName);
        await fileService.WriteAsync(newRecord.FileName, file, "Order");
        newRecord.AddUserId = httpContext.GetUserId();
        newRecord.OrderStatusId = OrderStatusesEnum.Card.ToInt();
        if (vm.DiscountId != null)
        {
            var discountDto = await appDbContext.Discounts.ProjectToType<DiscountDto>().FirstAsync(x => x.Id == vm.DiscountId);
            newRecord.PriceAfterDiscount = (newRecord.Price * newRecord.Series).CalculatePriceAfterDiscount(discountDto);
        }
        appDbContext.Orders.Add(newRecord);
        if (vm.DiscountId != null)
        {
            appDbContext.UserDiscounts.Add(new UserDiscount
            {
                UserId = httpContext.GetUserId(),
                DiscountId = vm.DiscountId.Value,
            });
        }
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(OrderEditVm vm)
    {
        await appDbContext.Orders.Where(x => x.Id == vm.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.OrderStatusId, vm.OrderStatusId));
        if (vm.OrderStatusId == OrderStatusesEnum.SendToPrint.ToInt())
            _ = smsService.SendChangeOrderStatus3(vm.AddUserMobile!, vm.Id.ToString());
        if (vm.OrderStatusId == OrderStatusesEnum.SendToCustomer.ToInt())
            _ = smsService.SendChangeOrderStatus4(vm.AddUserMobile!, vm.Id.ToString());
        return 1;
    }

    public async Task<int> Delete(int id)
    {
        var record = await appDbContext.Orders.AsNoTracking().FirstAsync(x => x.Id == id);
        fileService.Delete(record.FileName, record.FileExtension, "Order");
        if (record.DiscountId != null)
            await appDbContext.UserDiscounts.Where(x => x.UserId == httpContext.GetUserId() && x.DiscountId == record.DiscountId).ExecuteDeleteAsync();
        return await appDbContext.Orders.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task<int> ConfirmPayAndSendToMyOrdersBatch(ConfirmPayRequestVm vm)
    {
        return await appDbContext.Orders.Where(x => vm.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.OrderStatusId, OrderStatusesEnum.Pay.ToInt())
                .SetProperty(x => x.PayRefNumber, vm.RefNumber)
                .SetProperty(x => x.PayDate, vm.paidAt)
            );
    }
}