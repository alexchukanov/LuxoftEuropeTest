insert into Positions values (1, 'TV', 100);
insert into Positions values (2, 'Playstation', 120);
insert into Positions values (3, 'Table', 20);
insert into Positions values (4, 'PC', 80);
insert into Positions values (5, 'Chair', 10);
-----------------------------------------------------
insert into Clients values (1, 'Commander Shepard');
insert into Clients values (2, 'Adam Jensen');
insert into Clients values (3, 'JC Denton');
insert into Clients values (4, 'Tony Stark');
-----------------------------------------------------
insert into Orders values (1, 1, '21000101', NULL);
insert into Orders values (2, 1, '21000205', '21000306');

insert into Orders values (3, 2, '20271121', NULL);

insert into Orders values (4, 3, '20771010', '21000306');

insert into Orders values (5, 4, '20000615', NULL);
insert into Orders values (6, 4, '20000817', NULL);
insert into Orders values (7, 4, '20031102', '20030115');
-----------------------------------------------------
insert into OrderDetails values (1, 1, 1, 1);
insert into OrderDetails values (2, 1, 3, 1);
insert into OrderDetails values (3, 2, 4, 1);
insert into OrderDetails values (4, 2, 5, 1);

insert into OrderDetails values (5, 3, 2, 1);
insert into OrderDetails values (6, 3, 4, 1);

insert into OrderDetails values (7, 4, 5, 1);

insert into OrderDetails values (8, 5, 1, 1);
insert into OrderDetails values (9, 5, 3, 1);
insert into OrderDetails values (10, 5, 5, 1);
insert into OrderDetails values (11, 7, 5, 1);