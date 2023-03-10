# 3D-Pixel-Art-in-Unity
Project by: Гусамов Артур (РИ-210950), Утенков Руслан (РИ-210940)

![image](https://user-images.githubusercontent.com/77449049/207890956-f87ee465-824f-4b93-b877-c0d3689ba255.png)
![image](https://user-images.githubusercontent.com/77449049/207891219-00ce08fb-491f-4c25-a1f4-3f48a88fe295.png)

Импорт картинки небольшого разрешения с помощью python в unity
Изначально идея была в том, чтобы пиксель арт рисовался моделью обученной с помощью нейронной сети, но с этим возникли проблемы,
и в итоговой версии проекта картинка рисуется при помощи специально скрипта

## Python
- Чтобы код заработал у вас, нужно будет скачать json файл(он есть в списке файлов)
- Для получения цвета и координат используется следующий код:
```py
from PIL import Image
import gspread


gc = gspread.service_account(filename='daprojectgu-571cc82a3900.json') #Для корректной работы с Excel была настроена связь через cloud.Google
sh = gc.open('DAprojectGU')
sh.sheet1.clear()


def generate_coord_and_rgb():
    img = Image.open('heart.png') #Тут можно вставить свой пиксель арт(для корректного отображения стоит использовать арты не более 16x16 пикселей)
    size = w, h = img.size
    data = img.load()
    pieces = []
    for x in range(w):
        for z in range(h):
            r, g, b, a = data[x, z]
            if a == 0:
                continue
            pieces.append([x - 4.5, z - 4.5, r, g, b])
    pieces.sort(key=lambda arr: abs(arr[0]) + abs(arr[1]), reverse=True)
    end = f'E{len(pieces) + 1}'
    sh.sheet1.update(f'A1:{end}', pieces)


generate_coord_and_rgb()
```
- Если есть проблемы с API можете сами настроть работу с service.cloud.google, excel и python. Вероятнее всего вас этому научат на курсе:)
## Unity
- Для того, чтобы всё работало, нужно просто выбрать цель из которой будет рисоваться картинка и запустить сцену

![image](https://user-images.githubusercontent.com/77449049/207891540-212a3473-fe5c-459e-9797-6ffdaa500904.png)

- При желании можно изменить ссылку на свою таблицу и апи в коде
