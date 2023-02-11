# RefRetusa

# Embedded functions
onTask (TASK)
- condition
- actions (list of stringlify actions)
```yml
$onTask(COMPILE):
  condition: $(configuration) == "Release"
  actions:
    - $nope()
    - $nope()
```
