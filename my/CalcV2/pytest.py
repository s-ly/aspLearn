import requests
import sys


def rec():
    response = requests.get("http://localhost:5224")
    PrintRecuest(response)


def rec_result():
    response = requests.get("http://localhost:5224/result")
    PrintRecuest(response)


def rec_arg():
    response = requests.get("http://localhost:5224/arguments")
    PrintRecuest(response)


def recPost():
    data = {"X": "asd", "Y": 6}
    response = requests.post(
        "http://localhost:5224/setArg",
        json=data,
        headers={"Content-Type": "application/json"},
    )
    PrintRecuest(response)


def PrintRecuest(response):
    # print(response.json())
    print("Status:", response.status_code)
    # print("Headers:", response.headers)
    # print("Content-Type:", response.headers.get('Content-Type'))
    print("Body:", response.text)


if __name__ == "__main__":
    print()
    # нет аргументов (на самом деле имя это первый аргумент)
    if len(sys.argv) < 2:
        print("Использование: python pytest.py <метод>")
        print("Доступные методы: rec, rec_arg, recPost")
    else:
        method = sys.argv[1]
        if method == "rec":
            rec()
        elif method == "rec_arg":
            rec_arg()
        elif method == "recPost":
            recPost()
        elif method == "rec_result":
            rec_result()
        else:
            print(f"Неизвестный метод: {method}")
            print("Доступные методы: rec, rec_arg, recPost, rec_result")
