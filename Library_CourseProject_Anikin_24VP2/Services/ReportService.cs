using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Library_CourseProject_Anikin_24VP2.Services
{
    public class ReportService
    {
        /// <summary>
        /// Создание отчета в pdf
        /// </summary>
        /// <param name="path">Целевой путь</param>
        /// <param name="databaseName">Название БД</param>
        /// <param name="bookService">Сервис работы с книгами</param>
        /// <param name="clientService">Сервис работы с клиентами</param>
        /// <param name="borrowingService">Сервис работы с выдачами</param>
        public void GenerateReport(string path, string databaseName, BookService bookService, ClientService clientService, BorrowingService borrowingService)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            TextStyle.Default.FontFamily("Segou UI");

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(column =>
                    {
                        column.Item()
                        .PaddingBottom(10).AlignCenter().Text("Отчёт программы управления ИС «Библиотека»")
                        .SemiBold().FontSize(20).FontColor(Colors.BlueGrey.Darken3);

                        column.Item()
                        .PaddingBottom(5).AlignRight().Text($"База данных: {databaseName}")
                        .SemiBold().FontSize(16).FontColor(Colors.BlueGrey.Darken2);

                        column.Item()
                        .PaddingBottom(10).AlignRight().Text($"Дата формирования отчёта: {DateTime.Now:dd.MM.yyyy}")
                        .FontSize(14).FontColor(Colors.BlueGrey.Darken2);
                    });

                    page.Content().Column(column =>
                    {
                        var books = bookService.ReadAll();

                        column.Item()
                        .PaddingBottom(5).Text("Книги").FontSize(16).SemiBold().FontColor(Colors.BlueGrey.Darken3);

                        column.Item()
                        .PaddingBottom(10).Text($"{books.Count} записей").FontSize(12).SemiBold().FontColor(Colors.BlueGrey.Darken2);

                        column.Item().PaddingBottom(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Id");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Название");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Автор");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Год издания");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Жанр");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Количество");
                            });

                            foreach (var book in books)
                            {
                                table.Cell().Padding(5).AlignRight().Text(book.Id.ToString());
                                table.Cell().Padding(5).AlignLeft().Text(book.Title);
                                table.Cell().Padding(5).AlignLeft().Text(book.Author);
                                table.Cell().Padding(5).AlignCenter().Text(book.PublishYear.ToString());
                                table.Cell().Padding(5).AlignLeft().Text(book.Genre);
                                table.Cell().Padding(5).AlignCenter().Text(book.AvailableCount.ToString());
                            }
                        });

                        var clients = clientService.ReadAll();

                        column.Item()
                        .PaddingBottom(5).Text("Читатели").FontSize(16).SemiBold().FontColor(Colors.BlueGrey.Darken3);

                        column.Item()
                        .PaddingBottom(10).Text($"{clients.Count} записей").FontSize(12).SemiBold().FontColor(Colors.BlueGrey.Darken2);

                        column.Item().PaddingBottom(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Id");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("ФИО");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Телефон");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Адрес");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Дата регистрации");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Должник");
                            });

                            foreach (var client in clients)
                            {
                                table.Cell().Padding(5).AlignRight().Text(client.Id.ToString());
                                table.Cell().Padding(5).AlignLeft().Text(client.FullName);
                                table.Cell().Padding(5).AlignLeft().Text(client.Phone);
                                table.Cell().Padding(5).AlignLeft().Text(client.Address);
                                table.Cell().Padding(5).AlignCenter().Text(client.RegistrationDate.ToShortDateString());
                                table.Cell().Padding(5).AlignCenter().Text(client.IsDebtor ? "✔️" : "");
                            }
                        });

                        var borrowings = borrowingService.ReadAll();

                        column.Item()
                        .PaddingBottom(5).Text("Выдачи книг").FontSize(16).SemiBold().FontColor(Colors.BlueGrey.Darken3);

                        column.Item()
                        .PaddingBottom(10).Text($"{borrowings.Count} записей").FontSize(12).SemiBold().FontColor(Colors.BlueGrey.Darken2);

                        column.Item().PaddingBottom(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Id");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Читатель");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Книга");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Дата выдачи");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Срок");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Дата возврата");
                                header.Cell().Background(Colors.Amber.Lighten5).Padding(5).AlignCenter().Text("Возвращена");
                            });

                            foreach (var borrowing in borrowings)
                            {
                                string? client = clientService.Read(borrowing.ClientId)?.ToString();
                                string? book = bookService.Read(borrowing.BookId)?.ToString();

                                table.Cell().Padding(5).AlignRight().Text(borrowing.Id.ToString());
                                table.Cell().Padding(5).AlignLeft().Text(client);
                                table.Cell().Padding(5).AlignLeft().Text(book);
                                table.Cell().Padding(5).AlignCenter().Text(borrowing.BorrowDate.ToShortDateString());
                                table.Cell().Padding(5).AlignCenter().Text(borrowing.DueDate.ToShortDateString());
                                table.Cell().Padding(5).AlignCenter().Text(borrowing.ReturnDate?.ToShortDateString());
                                table.Cell().Padding(5).AlignCenter().Text(borrowing.IsReturned ? "✔️" : "");
                            }
                        });
                    });

                    page.Footer()
                    .AlignRight().Text(x => x.CurrentPageNumber());
                });
            })
            .GeneratePdf(path);
        }
    }
}
