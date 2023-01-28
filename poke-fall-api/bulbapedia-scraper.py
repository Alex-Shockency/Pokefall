# Packages
from bs4 import BeautifulSoup
import urllib
import re
import pandas as pd
import numpy as np
import requests
from urllib.request import urlopen
import csv

# Set-up
url = "https://bulbapedia.bulbagarden.net/"
pageurl = url + "wiki/List_of_Pokémon_by_National_Pokédex_number"
response = requests.get(pageurl)

pokeList = []

page = response.text
soup = BeautifulSoup(page, 'html.parser')

all_matches = soup.find_all('table', attrs={'class': ['roundy']})
for i in all_matches:
    list = ([a.attrs.get('href')
            for a in soup.select('table td a[title*="(Pok"]')])
    for x in list:
        pokeList.append(url + x)

# for x in set(pokeList):
#    print(x)

# Start of Script
no = []
name = []
generation = []
primary = []
secondary = []
hp = []
atk = []
defense = []
spatk = []
spdef = []
spd = []
bst = []
ability1 = []
ability2 = []
hiddenAbility = []
genderRatioFemale = []
genderRatioMale = []

pokeListSet = set(pokeList)
counter = 0
# Scraping Bulbapedia
for x in pokeListSet:  # Pokemon total 953
    p_url = x
    response = requests.get(p_url)
    p_page = response.text
    p_soup = BeautifulSoup(p_page, 'html.parser')

    # Find pokemon dex number
    no.append(p_soup.th.big.a.span.text)

    # Find pokemon name
    pokeName = p_soup.find('td', attrs={'width': ['50%']}).big.b.text
    name.append(pokeName)

    # Find Generation
    g = p_soup.select('ul li span a[class*="external text"] ')
    g = BeautifulSoup(str(g)).get_text()
    g = g[:-1][1:]
    g = g.split(",")
    generation.append(g[0])

    # Find typing
    t = p_soup.select('td a[href*="(type)"] span b')
    t = BeautifulSoup(str(t)).get_text()
    t = t[:-1][1:]
    t = t.split(",")
    primary.append(t[0])
    secondary.append(t[1])

    # Find abilities
    abilities = p_soup.select('td[style*="none"]')
    for abilityTag in abilities:
        abilityTag.decompose()
    abilities = p_soup.select('td[style="line-height:90%; font-size: 90%;"]')
    for abilityTag in abilities:
        abilityTag.decompose()

    ha = None
    for haTag in p_soup.select('td small'):
        if 'Hidden Ability' in haTag.get_text().strip():
            ha = BeautifulSoup(str(haTag.parent)).select(
                'td a[href*="(Ability)"] span')
            haTag.parent.decompose()

    try:
        setHiddenAbilities = set(ha)
        setHiddenAbilities = BeautifulSoup(str(setHiddenAbilities)).get_text().replace(
            '{', '').replace('}', '').split(',')
        hiddenAbility.append(setHiddenAbilities[0])
    except TypeError:
        hiddenAbility.append("")

    a = p_soup.select('td a[href*="(Ability)"] span')
    setAbilities = set(a)
    setAbilities = BeautifulSoup(str(setAbilities)).get_text().replace(
        '{', '').replace('}', '').split(',')
    ability1.append(setAbilities[0])
    if len(setAbilities) >= 2:
        ability2.append(setAbilities[1])
    else:
        ability2.append("")

    # Find Gender Ratio
    femaleOnly = p_soup.select('td a[href*="Female-only"]')
    if len(femaleOnly) > 0:
        genderRatioFemale.append(100)

    maleOnly = p_soup.select('td a[href*="Male-only"]')
    if len(maleOnly) > 0:
        genderRatioMale.append(100)

    halfRatio = p_soup.select(
        'td a[href*="gender_ratio_of_one_male_to_one_female"]')
    if len(halfRatio) > 0:
        genderRatioFemale.append(50)
        genderRatioMale.append(50)

    threeToOne = p_soup.select(
        'td a[href*="gender_ratio_of_three_males_to_one_female"]')
    if len(threeToOne) > 0:
        genderRatioFemale.append(25)
        genderRatioMale.append(75)

    sevenToOne = p_soup.select(
        'td a[href*="gender_ratio_of_seven_males_to_one_female"]')
    if len(sevenToOne) > 0:
        genderRatioFemale.append(12.5)
        genderRatioMale.append(87.5)

    oneToThree = p_soup.select(
        'td a[href*="gender_ratio_of_one_male_to_three_females"]')
    if len(oneToThree) > 0:
        genderRatioFemale.append(75)
        genderRatioMale.append(25)

    oneToSeven = p_soup.select(
        'td a[href*="gender_ratio_of_one_male_to_seven_females"]')
    if len(oneToSeven) > 0:
        genderRatioFemale.append(87.5)
        genderRatioMale.append(12.5)

    if len(femaleOnly) == 0 and len(halfRatio) == 0 and len(threeToOne) == 0 and len(sevenToOne) == 0 and len(oneToThree) == 0 and len(oneToSeven) == 0:
        genderRatioFemale.append(0)
    if len(maleOnly) == 0 and len(halfRatio) == 0 and len(threeToOne) == 0 and len(sevenToOne) == 0 and len(oneToThree) == 0 and len(oneToSeven) == 0:
        genderRatioMale.append(0)

    # Find stats
    stats = p_soup.findAll(
        'th', attrs={'style': ['width:85px; padding-left:0.5em; padding-right:0.5em']})
    stats = ([x.text for x in stats])

    # Keep only the stats numbers and store into a list
    store = []
    for x in stats:
        store.append(re.findall(r'[0-9]?[0-9]?[0-9]?[0-9]', x))

    # Removing brackets and converting stats into integer
    tempStats = []
    for x in store:
        x = int((str(x))[:-2][2:])
        tempStats.append(x)


    tableRows = p_soup.select('tr[style*="none"]')
    for row in tableRows:
        row.decompose()
    
    formsHtml = p_soup.select('tr td small')
    forms = []
    for form in formsHtml:
        if form.parent.has_attr('colspan') and not form.next.name == "b":
            if form.get_text() != pokeName and form.get_text() != "":
                forms.append(form.get_text().replace(u'\xa0', u' '))
    forms = set(forms)
    print(forms)
    # Store stats into appropriate list
    hp.append(tempStats[0])
    atk.append(tempStats[1])
    defense.append(tempStats[2])
    spatk.append(tempStats[3])
    spdef.append(tempStats[4])
    spd.append(tempStats[5])
    bst.append(tempStats[6])
    counter += 1
    print(counter)
    #if counter == 250:
    #    break

pokemon = {'Dex No.': no,
           'Name': name,
           'Generation': generation,
           'Primary Type': primary,
           'Secondary Type': secondary,
           'Ability1': ability1,
           'Ability2': ability2,
           'Hidden Ability': hiddenAbility,
           'Gender Ratio Male': genderRatioMale,
           'Gender Ratio Female': genderRatioFemale,
           'Health': hp,
           'Attack': atk,
           'Defense': defense,
           'Sp. Attack': spatk,
           'Sp. Defense': spdef,
           'Speed': spd,
           'BST': bst
           }

# Create Dataframe
df = pd.DataFrame.from_dict(pokemon)

# Write Csv
df.to_csv('bulbapedia_data.csv', index=None, header=True)
