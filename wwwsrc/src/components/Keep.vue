<template>
  <div class="rounded border border-primary bg-secondary">
    <img class="img-fluid rounded-top" :src="keepData.img" alt />
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