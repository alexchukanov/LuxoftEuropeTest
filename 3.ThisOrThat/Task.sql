--Given the associated database structure write an SQL query that lists names of the clients who ordered both X (position.Name = X) and Y (position.Name = Y) (not necessary as a part of the same order)
--but recieved goods (order is completed) from only one of these categories (e.g. either only X or only Y)
--E.g. client A has one pending order for X and one completed order for Y. He should appear in the output.
--Client B has one completed order for both X and Y. He should not appear in the output

-- Solution:
DECLARE @X varchar(25) , @Y varchar(25) SET @X = 'TV'SET @Y = 'Chair'

SELECT *   FROM  (SELECT c.Id, c.Name client, p.Name p, o.CompletionDate
                  FROM Clients c JOIN  Orders o ON  c.Id = o.ClientId    
                    INNER  JOIN OrderDetails od ON od.OrderId = o.Id    
                    INNER JOIN Positions p ON p.Id = od.PositionId     
                       WHERE  p.Name = @X  ) tbl1,
(SELECT c.Id, c.Name client, p.Name p, o.CompletionDate 
                  FROM Clients c JOIN  Orders o ON  c.Id = o.ClientId    
                     INNER  JOIN OrderDetails od ON od.OrderId = o.Id    
                     INNER JOIN Positions p ON p.Id = od.PositionId     
                        WHERE  p.Name = @Y ) tbl2
                              WHERE tbl1.Id = tbl2.Id
                                    AND ((tbl1.CompletionDate IS NOT NULL AND tbl2.CompletionDate IS NULL)
                                    OR  (tbl1.CompletionDate IS NULL AND tbl2.CompletionDate IS NOT NULL))  
