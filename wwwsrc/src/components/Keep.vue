<template>
  <div class="rounded mb-1 p-1 bg-dark text-light d-flex flex-column align-items-center">
    <img class="img-fluid rounded" :src="keepData.img" alt />
    <div>
      <h4>{{keepData.name}}</h4>
      <p>{{keepData.description}}</p>
    </div>
  </div>
</template>
<script>
export default {
  props: ["keepData"]
};
</script>
<style scoped>
</style>