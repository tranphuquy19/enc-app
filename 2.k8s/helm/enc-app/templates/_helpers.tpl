{{/* Generate basic labels */}}
{{- define "enc-app.labels" -}}
app.kubernetes.io/name: {{ .Chart.Name }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

{{/* Common labels */}}
{{- define "enc-app.commonLabels" -}}
{{ include "enc-app.labels" . }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Redis connection string
*/}}
{{- define "enc-app.redisConnection" -}}
{{- if .Values.viewService.redis.useInternal -}}
{{- printf "%s.%s.svc.cluster.local:%d" .Values.redis.name .Release.Namespace (int .Values.redis.service.port) -}}
{{- else -}}
{{- .Values.viewService.redis.connection -}}
{{- end -}}
{{- end }}

{{/*
Should deploy Redis
*/}}
{{- define "enc-app.shouldDeployRedis" -}}
{{- if and .Values.viewService.redis.useInternal (not .Values.viewService.redis.connection) -}}
{{- true -}}
{{- else -}}
{{- false -}}
{{- end -}}
{{- end }}

{{/*
Get resources configuration with defaults
*/}}
{{- define "enc-app.getResources" -}}
{{- $serviceResources := . -}}
{{- if $serviceResources -}}
{{- toYaml $serviceResources -}}
{{- else -}}
{{- toYaml $.Values.defaults.resources -}}
{{- end -}}
{{- end -}}

{{/*
Get autoscaling configuration with defaults
*/}}
{{- define "enc-app.getAutoscaling" -}}
{{- $serviceAutoscaling := . -}}
{{- if $serviceAutoscaling -}}
{{- toYaml $serviceAutoscaling -}}
{{- else -}}
{{- toYaml $.Values.defaults.autoscaling -}}
{{- end -}}
{{- end -}}
