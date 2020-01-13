Парсер на WPF, который позволяет скачивать и сохранять базу данных угроз с сайта
Программа создает локальную базу данных угроз безопасности информации, путем загрузки и последующего парсинга информации из официального банка данных угроз ФСТЭК России. Каждая запись об угрозе безопасности информации включает в себя следующие сведения об угрозе:
a. Идентификатор угрозы;
b. Наименование угрозы;
c. Описание угрозы;
d. Источник угрозы;
e. Объект воздействия угрозы;
f. Нарушение конфиденциальности (да\нет);
g. Нарушение целостности (да\нет);
h. Нарушение доступности (да\нет).
Имеет функцию обновления сведений. В результате обновления, программа выводит пользователю отчет об обновлении, с указанием:
a. Статуса обновления (Успешно\Ошибка);
b. Общего количества обновленных записей (если успешно, ну а если
произошла ошибка вывести сведения о причинах ошибки).
c. Идентификаторов измененных угроз и указанием состава измененной
информации (в формате «БЫЛО - СТАЛО»)