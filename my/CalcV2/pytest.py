# Отправка запросов на CalcV2

import requests
import sys


def map_hello():
    response = requests.get("http://localhost:5224")
    PrintRecuest(response)


def map_result():
    response = requests.get("http://localhost:5224/result")
    PrintRecuest(response)


def map_arguments():
    response = requests.get("http://localhost:5224/arguments")
    PrintRecuest(response)

def map_one_result():
    response = requests.get("http://localhost:5224/oneResult/5")
    PrintRecuest(response)


def map_post_set_arg():
    data = {"X": 55, "Y": 66}
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
    else:
        method = sys.argv[1]
        if method == "map_hello":
            map_hello()
        elif method == "map_arguments":
            map_arguments()
        elif method == "map_post_set_arg":
            map_post_set_arg()
        elif method == "map_one_result":
            map_one_result()
        elif method == "map_result":
            map_result()
        else:
            print(f"Неизвестный метод: {method}")
            print("Доступные методы:")
            print("map_hello, map_arguments, map_result, map_one_result, map_post_set_arg")
