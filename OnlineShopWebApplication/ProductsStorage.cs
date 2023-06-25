﻿using System.Xml.Linq;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class ProductsStorage
    {
        public List<Product> Products = new List<Product>
        {
            new Product("God of war", 2500, "Отправляйтесь в скандинавское царство\nОтомстив богам Олимпа, Кратос живет в царстве скандинавских божеств и чудовищ. В этом суровом беспощадном мире он должен не только самостоятельно бороться за выживание... но и научить этому сына.\n\nВторой шанс\nКратос снова отец. Будучи наставником и защитником сына, который стремится заслужить уважение отца, Кратос получил неожиданную возможность обуздать собственный гнев, который долгое время определял его поступки.\n\nПогружайтесь в мрачный мир грозных существ\nОт мраморных полов и роскошных колонн Олимпа до диких лесов, гор и пещер скандинавской мифологии задолго до появления викингов – вам предстоит оказаться в совершенно новых местах со своим пантеоном существ, чудовищ и богов.\n\nУчаствуйте в беспощадных сражениях\nС новой камерой за плечом главного героя игроки окажутся ближе к схваткам, чем когда-либо, а сами бои будут под стать скандинавским существам God of War™, с которыми столкнется Кратос: величественными, суровыми и неутомимыми. Новое основное оружие и способности хранят верность атмосфере серии God of War, давая возможность по-новому взглянуть на жестокие конфликты, присущие игровому жанру.", "~/img/GodOfWar.jpeg"),
            new Product("Cyberpunk 2077", 2000, "Безумству мира поем мы славу! И он действительно сошел с ума, да так, что фантазии Уильяма Гибсона отдыхают. Моральные ценности утратили актуальность для людей. В моде цифровые наркотики и кибернизация организма. Первые дают возможность жить чужими эмоциями, как легальными: счастье, оргазм, эйфория, так и не совсем. Вторые превращают людей в полных роботов, которых можно брать под контроль и использовать в любых целях.\n\nВ гнездо разврата и нищеты превратился симпатичный некогда городок Night City. Здесь правят наркоторговцы, черные кибертрансплантологи, бандитские группировки. Хотите узнать, как выживают в этом кибергадюшнике простые люди, и попробовать изменить ситуацию к лучшему? Тогда поторопитесь купить Cyberpunk 2077 на steampay.com. Конец света уже не за горами!","~/img/Cyberpunk.jpg"),
            new Product("Detroit: Become Human", 950, "Detroit: Become Human, отмеченная наградами видеоигра от Quantic Dream, наконец доступна в Steam! В создании игры приняли участие всемирно известные актеры, среди которых: Джесси Уильямс («Анатомия страсти»), Клэнси Браун («Карнавал»), Лэнс Хенриксен («Чужие»), Брайан Декарт («Настоящая кровь») и Вэлори Керри («Сумерки»).\n\nЧТО ДЕЛАЕТ НАС ЛЮДЬМИ?\nДетройт, 2038 год. Технологии развились до такой степени, что человекообразные андроиды встречаются на каждом шагу. Они говорят, двигаются, ведут себя словно человеческие существа, но они — лишь машины в на службе у людей.", "~/img/Detroit.jpg")
        };

        public List<Product> GetAll()
        {
            return Products;
        }

        public Product TryGetById(int? id)
        {
            return Products.FirstOrDefault(product => product.Id == id);
        }
    }
}
