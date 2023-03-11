# TicTacToeApi
POST /api/Game/CreateEdit

Создаёт (если json пустой) или редактирует партию

По аналогии с шахматной FEN используется строковое представление поля (position)
| 0 | 1 | 2 |
|---|---|---|
| 3 | 4 | 5 |
| 6 | 7 | 8 |

Например `?XO?XXO??` это 
| ? | X | O |
|---|---|---|
| ? | X | X |
| O | ? | ? |

Status игры -  `0,1,2,3` (In Progress, Draw, X Won, O Won)`

GET /api/Game/Get/:id

Возвращает игру по указанному id

DELETE /api/Game/Delete

Удаляет игру по id

GET /api/Game/GetAll

Возвращает список всех игр
