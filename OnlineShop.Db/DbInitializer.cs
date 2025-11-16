using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Db
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationContext context)
        {
            await context.Database.EnsureCreatedAsync();
            await InitializeProductsAsync(context);
        }

        private static async Task InitializeProductsAsync(ApplicationContext context)
        {
            var products = new List<Product>
            {
                new() {
                    Name = "Assassin's Creed: Odyssey",
                    Cost = 400,
                    ImgFileName = "ac_odyssey.jpg",
                    Description = "Эпическая ролевая игра в Древней Греции. Станьте спартанским наемником и определите судьбу своего героя в разгаре Пелопоннесской войны. Исследуйте огромный открытый мир, принимайте решения, влияющие на сюжет, и сражайтесь в масштабных битвах."
                },
                new() {
                    Name = "Atomic Heart",
                    Cost = 350,
                    ImgFileName = "atomic_heart.jpg",
                    Description = "Шутер от первого лица в альтернативной советской реальности. Оказавшись в секретном научном комплексе, где роботы вышли из-под контроля, вы должны раскрыть тайну катастрофы. Уникальная атмосфера, интересный геймплей и захватывающий сюжет."
                },
                new() {
                    Name = "Cyberpunk 2077",
                    Cost = 450,
                    ImgFileName = "cyberpunk_2077.jpg",
                    Description = "Научно-фантастическая ролевая игра в открытом мире мегаполиса Найт-Сити. Сыграйте за V - наемника в поисках уникального импланта, дарующего бессмертие. Глубокий сюжет, свобода выбора и неоновые улицы будущего."
                },
                new() {
                    Name = "Death Stranding",
                    Cost = 380,
                    ImgFileName = "death_stranding.jpg",
                    Description = "Экшен-приключение от легендарного Хидео Кодзимы. В постапокалиптическом мире доставьте грузы через опасные территории, объединяя разрозненные поселения. Уникальный геймплей, философский сюжет и звездный актерский состав."
                },
                new() {
                    Name = "Detroit: Become Human",
                    Cost = 420,
                    ImgFileName = "detroit_become_human.jpg",
                    Description = "Интерактивная драма о будущем, где андроиды обретают сознание. Принимайте судьбоносные решения за трех персонажей-роботов, от которых зависит будущее человечества. Каждое ваше решение меняет ход истории."
                },
                new() {
                    Name = "Divinity: Original Sin 2",
                    Cost = 320,
                    ImgFileName = "divinity_original_sin_2.jpg",
                    Description = "Тактическая ролевая игра с глубокой боевой системой и нелинейным сюжетом. Соберите команду и исследуйте богатый фэнтезийный мир. Свобода действий, сложные диалоги и тактические сражения."
                },
                new() {
                    Name = "Elden Ring",
                    Cost = 480,
                    ImgFileName = "elden_ring.jpeg",
                    Description = "Эпическая action-RPG от создателей Dark Souls. Исследуйте огромный открытый мир Межземья, сражайтесь с легендарными боссами и раскройте тайны Древних Колец. Сложный геймплей и захватывающая атмосфера."
                },
                new() {
                    Name = "Forza Horizon 4",
                    Cost = 370,
                    ImgFileName = "forza_horizon_4.jpg",
                    Description = "Гоночный симулятор в открытом мире Великобритании. Участвуйте в масштабном фестивале Horizon, собирайте коллекцию автомобилей и наслаждайтесь красотами сменяющихся времен года. Реалистичная физика и потрясающая графика."
                },
                new() {
                    Name = "God of War",
                    Cost = 460,
                    ImgFileName = "god_of_war.jpeg",
                    Description = "Экшен-приключение о Кратосе и его сыне Атрее. Отправьтесь в опасное путешествие по скандинавским мифам, сражайтесь с легендарными чудовищами и раскройте семейные тайны. Глубокий сюжет и кинематографичная подача."
                },
                new() {
                    Name = "Northgard",
                    Cost = 280,
                    ImgFileName = "northgard.jpg",
                    Description = "Стратегия в реальном времени по мотивам скандинавской мифологии. Возглавьте клан викингов в освоении таинственных земель Нортгарда. Развивайте поселение, сражайтесь с врагами и переживайте суровые зимы."
                },
                new() {
                    Name = "Red Dead Redemption 2",
                    Cost = 500,
                    ImgFileName = "red_dead_redemption_2.jpg",
                    Description = "Эпический вестерн от создателей GTA. Погрузитесь в жизнь бандита Артура Моргана в эпоху дикого запада. Исследуйте огромный открытый мир, участвуйте в перестрелках и принимайте моральные решения, влияющие на судьбу героя."
                }
            };

            foreach (var product in products)
            {
                if (!await context.Products.AnyAsync(entity => entity.Name == product.Name))
                {
                    await context.Products.AddAsync(product);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}