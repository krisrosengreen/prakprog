from bs4 import BeautifulSoup as bs
import os
import requests
from termcolor import colored

SITE = "http://212.27.24.106:8080/prog/"
EXERCISE_FOLDER = "exercises/"
HOMEWORKS_FOLDER = "homeworks/"

site_content = requests.get(SITE).text
soup = bs(site_content, features="html5lib")
lectures = soup.find_all("li", {"style": "color:black"})

def extract_folder_name(link):
    return link.split("/")[-1].split(".")[0]

exercises = []
homeworks = []

for lecture in lectures:
    links = lecture.find_all("a")
    for link in links:
        if link.has_attr("href"):
            href_content = link["href"]
            if "exercises" in href_content:
                exercises.append(extract_folder_name(href_content))
            elif "homeworks" in href_content:
                homeworks.append(extract_folder_name(href_content))

current_exercises = os.listdir(EXERCISE_FOLDER)
current_homeworks = os.listdir(HOMEWORKS_FOLDER)

included_L = lambda s, L: len(list(filter(lambda i: s in i, L))) >= 1

def check_min_requirement(path):
    files = os.listdir(path)
    return included_L(".cs", files) and "Makefile" in files

def check_no_missing(path):
    return "MISSING" in os.listdir(path)

def warning(s):
    print(colored(s, 'yellow'))

def error(s):
    print(colored(s, 'red'))

def good(s):
    print(colored(s, 'green'))

def underline(s):
    print(s)
    print('―'*len(s))

def file_tab_print(filepath):
    with open(filepath, 'r') as f:
        content = f.read().strip()
    adj = list(map(lambda i: "\t* " + i, content.split("\n")))
    print()
    print("\n".join(adj))
    print()

spacing = 10

underline(f"Exercises ({len(current_exercises)}/{len(exercises)})")
for exercise in exercises:
    if exercise not in current_exercises:
        error(f" - {exercise: <{spacing}} not done!")
    else:
        if not check_min_requirement(EXERCISE_FOLDER + exercise):
            error(f" - {exercise: <{spacing}} does not pass min requirements!")
        elif check_no_missing(EXERCISE_FOLDER + exercise):
            warning(f" - {exercise: <{spacing}} has a MISSING file!")
            file_tab_print(EXERCISE_FOLDER + exercise + "/MISSING")
        else:
            good(f" - {exercise: <{spacing}} passes")

print()
underline(f"Homeworks ({len(current_homeworks)}/{len(homeworks)})")
for homework in homeworks:
    if homework not in current_homeworks:
        error(f" - {homework: <{spacing}} not done!")
    else:
        if not check_min_requirement(HOMEWORKS_FOLDER+ homework):
            error(f" - {homework: <{spacing}} does not pass min requirements!")
        elif check_no_missing(HOMEWORKS_FOLDER+ homework):
            warning(f" - {homework: <{spacing}} has a MISSING file!")
            file_tab_print(HOMEWORKS_FOLDER + homework + "/MISSING")
        else:
            good(f" - {homework: <{spacing}} passes")
