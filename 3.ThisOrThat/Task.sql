﻿--Given the associated database structure write an SQL query that lists names of the clients who ordered both X (position.Name = X) and Y (position.Name = Y) (not necessary as a part of the same order)
--but recieved goods (order is completed) from only one of these categories (e.g. either only X or only Y)
--E.g. client A has one pending order for X and one completed order for Y. He should appear in the output.
--Client B has one completed order for both X and Y. He should not appear in the output