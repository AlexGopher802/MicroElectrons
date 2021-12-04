use MicroElectronsDB;

drop trigger if exists insert_supply;
create trigger insert_supply after insert
    on SupplyCompos for each row begin
    insert into ComeJournal(subjectName, quantity, dateTimeConfirm, isCome, operationId) values
        ((select name from Product where Product.id=NEW.productId),
         (NEW.quantity),
         (select dateSupply from Supply where Supply.id=NEW.supplyId),
         not(select isSell from Supply where Supply.id=NEW.supplyId),
         (select id from OperationType where name='Поставки'));

    update Warehouse set quantity = quantity + if((select isSell from Supply where Supply.id=NEW.supplyId), -NEW.quantity, NEW.quantity)
    where productId=NEW.productId;
end;

drop trigger if exists update_task;
create trigger update_task after update
    on Task for each row begin
    insert into ComeJournal(subjectName, quantity, dateTimeConfirm, isCome, operationId) values
        ((select name from Product where Product.id=(select productId from Warehouse where Warehouse.id=NEW.warehouseId)),
         (NEW.quantity),
         (select dateEnd from Task where Task.id=NEW.id),
         1,
         (select id from OperationType where name='Производство'));

    update Warehouse set quantity = quantity + NEW.quantity
    where Warehouse.id=NEW.warehouseId;
end;

create trigger insert_product after insert
    on Product for each row begin
    insert into Warehouse(productId, quantity, storageMethodId) values
        (NEW.id, 0, 1);
end;