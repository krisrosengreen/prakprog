import sys
import os

base_url = "http://212.27.24.106:8080/prog/"
os.chdir("/home/kristoffer/Documents/prakprog/")

def str_append(s):
    return list(map(lambda i: s + "/" + i, os.listdir(s)))

if len(sys.argv) == 2:
    exercises = str_append("exercises")
    homeworks = str_append("homeworks")

    total = exercises + homeworks
    L_res = list(filter(lambda i: sys.argv[1] in i, total))

    os.system("firefox " + base_url + L_res[0] + ".htm")
else:
    print("Incorrect format")
