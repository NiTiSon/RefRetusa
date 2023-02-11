# RefRetusa

# Functions
Every functions begins on the dollar sign ($)

## Embedded functions

### onTask (TASK)
- condition
- actions
```yml
$onTask(COMPILE):
  condition: $(configuration) == "Release"
  actions:
    - $nope()
    - $nope()
```

### if (CONDITION)
- actions
```yml
$if(true):
  actions:
    - $nope()
    - $nope()
```
