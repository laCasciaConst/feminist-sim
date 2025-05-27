import random
import pandas as pd

traits = ['authority', 'equality', 'individual', 'collective', 'cynicism']

# 질문별 4지선다 trait 점수 (예시는 단순값)
questions = [
    # Q1 ~ Q7 (2015)
    [{'authority': 1}, {'equality': 2, 'individual': 2}, {'cynicism': 3}, {'individual': 2, 'collective': 1}],
    [{'authority': 1}, {'equality': 2, 'individual': 1}, {'cynicism': 3}, {'equality': 1, 'collective': 2}],
    [{'individual': 2}, {'equality': 1, 'collective': 2}, {'cynicism': 3}, {'authority': 2}],
    [{'cynicism': 1,}, {'authority': 1}, {'equality': 3}, {'cynicism': 1, 'authority':2}],
    [{'individual': 2}, {'authority': 3, 'individual': 1}, {'equality': 3, 'collective': 1}, {'cynicism': 3}],
    [{'authority': 2}, {'equality': 2}, {'individual': 1}, {'authority': 1}],
    [{'equality': 1, 'individual': 1}, {'authority': 2}, {'cynicism': 2, 'collective': 3}],
     [{'authority': 5, 'individual': 2}, {'authority': 2, 'collective': 3}, {'individual': 4, 'equality': 3,}, {'equality': 3, 'collective': 4}],

    # Q8 (가중치 방향 선택용)
    [{'equality': 3, 'individual': 2}, {'authority': 3, 'individual': 2}, {'authority': 2, 'collective': 2}, {'equality': 2, 'collective': 2}],

    # Q9 ~ Q15 / Q16 ~ Q18 (가중치 적용 대상)
    [{'equality': 2, 'individual': 2}, {'authority': 2, 'individual': 2}, {'cynicism': 4}, {'collective': 1, 'authority': 2}],
    [{'authority': 4}, {'equality': 3, 'collective': 2}, {'individual': 3}, {'cynicism': 2}],
    [{'individual': 3, 'equality': 4,}, {'authority': 4, 'individual': 1}, {'cynicism': 4, 'individual': 2}, {'collective': 2, 'authority': 3}],
    [{'authority': 4, 'individual': 3}, {'authority': 3, 'collective': 2}, {'cynicism': 8, 'individual': 2}, {'equality': 4, 'individual': 2}],
    [{'authority': 4, 'individual': 3}, {'authority': 2, 'collective': 2}, {'individual': 3, 'equality': 4,}, {'equality': 4, 'collective': 3}],
    [{'equality': 4, 'individual': 4}, {'authority': 4, 'individual': 3}, {'equality': 4, 'collective': 3}, {'authority': 2, 'collective': 2}],
    [{'authority': 4, 'individual': 5}, {'equality': 3, 'individual': 5}, {'equality': 4, 'collective': 3}, {'collective': 3, 'authority': 5}],
    [{'authority': 3, 'individual': 4}, {'individual': 5}, {'cynicism': 5}, {'equality': 4, 'collective': 3}],
    [{'authority': 3, 'individual': 4}, {'equality': 3, 'individual': 4}, {'authority': 2, 'collective': 1}, {'cynicism': 7}],
]

N = 100000
cynicism_threshold = 22
results = []

for _ in range(N):
    score = {trait: 0 for trait in traits}

    # Q1 ~ Q7 (2015)
    for i in range(7):
        choice = random.choice(questions[i])
        for trait, val in choice.items():
            score[trait] += val

    # Q8 (가중치 방향 선택)
    q8_choice = random.choice(questions[7])
    dominant, secondary = sorted(q8_choice, key=lambda k: q8_choice[k], reverse=True)[:2]
    for trait, val in q8_choice.items():
        score[trait] += val

    # Q9~Q15 (2016 가중치 적용)
    for i in range(8, 15):
        choice = random.choice(questions[i])
        for trait, val in choice.items():
            if trait == 'cynicism':
                score[trait] += val
            elif trait == dominant:
                score[trait] += val * 4
            elif trait == secondary:
                score[trait] += val * 3
            else:
                score[trait] += val

    if score['cynicism'] >= cynicism_threshold:
        route = 'route6'
    else:
        r1 = score['authority'] + score['individual']
        r2 = score['authority'] + score['collective']
        r3 = score['equality'] + score['collective']
        r4 = score['equality'] + score['individual']
        route = max(
            {'route1': r1, 'route2': r2, 'route3': r3, 'route4': r4},
            key=lambda k: {'route1': r1, 'route2': r2, 'route3': r3, 'route4': r4}[k]
        )

    results.append((route, score.copy()))

# 분석
df = pd.DataFrame([{'route': r, **s} for r, s in results])
print(df['route'].value_counts(normalize=True).round(4) * 100)
print(df.groupby('route')[traits].mean().round(2))
