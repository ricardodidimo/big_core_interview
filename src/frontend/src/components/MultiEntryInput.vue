<script setup lang="ts">
import { ref, defineEmits, defineProps } from 'vue';

const propInput = ref("");
const props = defineProps<{ container: string[], label: string }>();
const emit = defineEmits(["update:container"]);

function addItem() {
    if (propInput.value.trim() && !props.container.includes(propInput.value)) {
        emit("update:container", [...props.container, propInput.value.trim()]);
        propInput.value = "";
    }
}

function removeItem(index: number) {
    const updatedContainer = [...props.container];
    updatedContainer.splice(index, 1);
    emit("update:container", updatedContainer);
}
</script>

<template>
    <div class="my-2">
        <v-text-field
            v-model="propInput"
            :label="props.label"
            dense
            hide-details
            @keydown.enter="addItem"
            append-inner-icon="fa fa-plus"
            @click:append-inner.stop="addItem"
        />

        <v-chip-group v-if="props.container.length" prev-icon="fa fa-arrow-left" next-icon="fa fa-arrow-right" show-arrows>
            <v-chip v-for="(item, index) in props.container" :key="index" close @click.stop="removeItem(index)">
                {{ item }}
            </v-chip>
        </v-chip-group>
    </div>
</template>
