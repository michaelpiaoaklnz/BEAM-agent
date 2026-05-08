#!/bin/bash

module load Python/3.11.6-foss-2023a

export OPENAI_API_BASE=http://127.0.0.1:8000/v1
export OPENAI_API_KEY=EMPTY

source ~/00_nesi_projects/aut04563_nobackup/venvs/beam-aider-env/bin/activate

cd ~/ai-agent-exp/BEAM-agent

aider \
  --model openai/Qwen/Qwen2.5-Coder-14B-Instruct \
  --openai-api-base http://127.0.0.1:8000/v1 \
  --openai-api-key EMPTY \
  --no-check-update \
  --no-show-model-warnings \
  --no-auto-commits
